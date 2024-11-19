using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from a target position
/// </summary>
public class Flee : SteeringBehaviourBase
{
    [Tooltip("The location we want to flee from")]
    [SerializeField] private Vector3 TargetPos;

    public Flee(Vector3 TargetPosition)
    {
        TargetPos = TargetPosition;
    }

    public void SetTargetPosition(Vector3 NewTargetPosition)
    {
        TargetPos = NewTargetPosition;
    }

    public override Vector3 Calculate()
    {
        Vector3 desiredVelocity = (transform.position - TargetPos).normalized * VehicleComponent.GetMaxSpeed();

        Vector3 steeringForce = desiredVelocity - VehicleComponent.GetVelocity();

        return steeringForce;
    }
}
