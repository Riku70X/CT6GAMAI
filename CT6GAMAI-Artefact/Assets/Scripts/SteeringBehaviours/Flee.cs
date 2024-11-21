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
        return GetFleeingForceFromLocation(VehicleComponent, transform.position, TargetPos);
    }

    /// <summary>
    /// Returns a force that directs the agent away from a target position
    /// </summary>
    /// <param name="TargetPosition">The location we are fleeing from.</param>
    /// <returns></returns>
    public static Vector3 GetFleeingForceFromLocation(Vehicle VehicleComponent, Vector3 CurrentPosition, Vector3 TargetPosition)
    {
        Vector3 desiredVelocity = (CurrentPosition - TargetPosition).normalized * VehicleComponent.GetMaxSpeed();

        Vector3 steeringForce = desiredVelocity - VehicleComponent.GetVelocity();

        return steeringForce;
    }
}
