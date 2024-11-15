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

    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();

        Vector3 desiredVelocity = (transform.position - TargetPos).normalized * vehicle.MaxSpeed;

        Vector3 steeringForce = desiredVelocity - vehicle.Velocity;

        return steeringForce;
    }
}
