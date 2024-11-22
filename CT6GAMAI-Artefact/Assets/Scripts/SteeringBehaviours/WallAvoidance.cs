using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from walls in its path
/// </summary>
public class WallAvoidance : SteeringBehaviourBase
{
    [Tooltip("The default length of the line traces."/*At runtime, it will be proportional to the speed of the agent."*/)]
    [SerializeField] private float BaseLineTraceLength = 5.0f;

    [Tooltip("The angle of the side detection whiskers (degrees)")]
    [SerializeField] private float SecondaryTraceAngleOffset = 40.0f;

    [Tooltip("The name of the Layer used by walls. This should match the Wall layer name in the project files ('Tags & Layers').")]
    private readonly string WallLayerName = "Wall";

    [Tooltip("The layer mask used by walls.")]
    private int WallLayerMask;

    protected override void Awake()
    {
        base.Awake();

        WallLayerMask = 1 << LayerMask.NameToLayer(WallLayerName);
    }

    public override Vector3 Calculate()
    {
        Vector3 steeringForce = Vector3.zero;

        Vector3 traceStartLocation = transform.position;

        float lineTraceLength = BaseLineTraceLength; /* * VehicleComponent.GetSpeed();*/

        for (int i = -1; i < 2; i++)
        {
            Vector3 traceDirection = transform.forward * lineTraceLength;

            traceDirection = Quaternion.Euler(0, i * SecondaryTraceAngleOffset, 0) * traceDirection;

            Vector3 traceEndLocation = traceStartLocation + traceDirection;

            Debug.DrawLine(traceStartLocation, traceEndLocation, Color.blue);

            if (Physics.Linecast(traceStartLocation, traceEndLocation, out RaycastHit hit, WallLayerMask))
            {
                float penetrationDistance = lineTraceLength - hit.distance;

                float forceMultipler = penetrationDistance / hit.distance;

                steeringForce += hit.normal * penetrationDistance;
            }

        }

        return steeringForce;
    }
}
