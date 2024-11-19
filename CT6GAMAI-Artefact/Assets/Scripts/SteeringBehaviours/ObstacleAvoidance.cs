using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from obstacles in its path
/// </summary>
public class ObstacleAvoidance : SteeringBehaviourBase
{
    [Tooltip("The width of the detection box, equal to the bounds of the agent")]
    [SerializeField] private float detectionBoxWidth = 0.0f;

    [Tooltip("The default length of the detection box. At runtime, it will be proportional to the speed of the agent")]
    [SerializeField] private float baseDetectionBoxLength = 0.0f;

    private void Awake()
    {
        Collider collider = GetComponent<Collider>();
        detectionBoxWidth = collider.bounds.extents.x;
    }

    public override Vector3 Calculate()
    {


        float detectionBoxLength = baseDetectionBoxLength * VehicleComponent.GetSpeed();

        BoxCollider detectionBox = gameObject.AddComponent<BoxCollider>();

        detectionBox.size = new Vector3(detectionBoxWidth, 0.0f, detectionBoxLength);
        detectionBox.center = new Vector3(0.0f, 0.0f, detectionBoxLength / 2);

        return Vector3.zero;
    }
}
