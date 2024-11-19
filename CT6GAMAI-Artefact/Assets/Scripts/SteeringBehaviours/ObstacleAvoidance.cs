using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from obstacles in its path
/// </summary>
public class ObstacleAvoidance : SteeringBehaviourBase
{
    [Tooltip("The default length of the detection box. At runtime, it will be proportional to the speed of the agent")]
    [SerializeField] private float BaseDetectionBoxLength = 1.0f;

    [Tooltip("The width of the detection box, equal to the bounds of the agent")]
    private float DetectionBoxWidth = 0.0f;

    [Tooltip("Reference to this gameObject's collider component")]
    private Collider ColliderComponent;

    protected override void Awake()
    {
        base.Awake();

        if (TryGetComponent(out ColliderComponent))
        {
            DetectionBoxWidth = ColliderComponent.bounds.extents.x;
        }
        else
        {
            Debug.LogError("ObstacleAvoidance::Awake() has failed - gameObject needs to have a collider component");
        }
    }

    public override Vector3 Calculate()
    {
        float detectionBoxLength = BaseDetectionBoxLength * VehicleComponent.GetSpeed();
        Vector3 detectionBoxSize = new Vector3(DetectionBoxWidth, 0.0f, detectionBoxLength);
        Vector3 detectionBoxCentre = new Vector3(0.0f, 0.0f, detectionBoxLength / 2);
        Quaternion detectionBoxRotation = Quaternion.Euler(transform.forward);

        Collider[] overlappingColliders = Physics.OverlapBox(transform.position + detectionBoxCentre, detectionBoxSize, detectionBoxRotation);

        foreach (Collider collider in overlappingColliders)
        {
            if (collider != ColliderComponent)
            {
                Debug.Log(name + " wants to avoid " + collider.name);
            }
        }

        return Vector3.zero;
    }
}
