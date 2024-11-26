using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent towards a target position
/// </summary>
public class Seek : SteeringBehaviourBase
{
    [Tooltip("The location we want to seek to")]
    [SerializeField] private Vector3 TargetPos;

    public override Vector3 Calculate()
    {
        return GetSeekingForceToLocation(VehicleComponent, transform.position, TargetPos);
    }

    /// <summary>
    /// Returns a force that directs the agent towards a target position
    /// </summary>
    /// <param name="TargetPosition">The location we are seeking to.</param>
    public static Vector3 GetSeekingForceToLocation(Vehicle VehicleComponent, Vector3 CurrentPosition, Vector3 TargetPosition)
    {
        Vector3 desiredVelocity = (TargetPosition - CurrentPosition).normalized * VehicleComponent.GetMaxSpeed();

        Vector3 steeringForce = desiredVelocity - VehicleComponent.GetVelocity();

        return steeringForce;
    }
}
