using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from walls in its path
/// </summary>
public class WallAvoidance : SteeringBehaviourBase
{
    [Tooltip("The default length of the line traces. At runtime, it will be proportional to the speed of the agent.")]
    [SerializeField] private float BaseLineTraceLength = 1.0f;

    public override Vector3 Calculate()
    {
        Vector3 steeringForce = Vector3.zero;

        Vector3 traceStartLocation = transform.position;

        float lineTraceLength = BaseLineTraceLength * VehicleComponent.GetSpeed();

        //for (int i = 0; i < 3; i++)
        //{
        Vector3 traceDirection = transform.forward * lineTraceLength;

        Vector3 traceEndLocation = traceStartLocation + traceDirection;

        Physics.Linecast(traceStartLocation, traceEndLocation, out RaycastHit hit);

        float penetrationDistance = lineTraceLength - hit.distance;

        steeringForce += hit.normal * penetrationDistance;

        //}

        return steeringForce;
    }
}
