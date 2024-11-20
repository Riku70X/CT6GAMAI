using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from obstacles in its path
/// </summary>
public class ObstacleAvoidance : SteeringBehaviourBase
{
    [Tooltip("The default length of the detection box. At runtime, it will be proportional to the speed of the agent. Measured from the centre.")]
    [SerializeField] private float BaseDetectionBoxLength = 1.0f;

    [Tooltip("The width of the detection box, equal to the width of the agent's collider. Measured from the centre.")]
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

        Vector3 detectionBoxExtents = new(DetectionBoxWidth, 0.0f, detectionBoxLength);

        Vector3 relativeDetectionBoxCentre = new(0.0f, 0.0f, detectionBoxLength);

        Quaternion detectionBoxRotation = Quaternion.Euler(transform.forward);

        Collider[] overlappingColliders = Physics.OverlapBox(transform.position + relativeDetectionBoxCentre, detectionBoxExtents, detectionBoxRotation);

        Collider closestCollider = overlappingColliders[0];

        float shortestDistance = Vector3.Distance(transform.position, closestCollider.ClosestPoint(transform.position));

        foreach (Collider collider in overlappingColliders)
        {
            if (collider != ColliderComponent) //ignore own collider
            {
                Debug.Log(name + " wants to avoid " + collider.name);

                float distance = Vector3.Distance(transform.position, collider.ClosestPoint(transform.position));

                if (distance < shortestDistance)
                {
                    closestCollider = collider;
                    shortestDistance = distance;
                }
            }
        }

        float forceMultiplier = 1 + (detectionBoxLength - shortestDistance) / detectionBoxLength;

        float steeringForce; // = trig maths for (-1 to 1) value, then * by forceMultiplier

        Vector3 STEERINTGAEGEA = transform.right * forceMultiplier;

        return STEERINTGAEGEA;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        float detectionBoxLength = BaseDetectionBoxLength * VehicleComponent.GetSpeed();

        Vector3 detectionBoxExtents = new(DetectionBoxWidth, 0.0f, detectionBoxLength);

        Vector3 relativeDetectionBoxCentre = new(0.0f, 0.0f, detectionBoxLength);

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(relativeDetectionBoxCentre, detectionBoxExtents * 2);
    }
}
