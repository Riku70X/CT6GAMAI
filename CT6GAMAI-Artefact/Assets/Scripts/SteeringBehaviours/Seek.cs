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
        Vehicle vehicle = GetComponent<Vehicle>();

        Vector3 desiredVelocity = (TargetPos - transform.position).normalized * vehicle.MaxSpeed;

        Vector3 steeringForce = desiredVelocity - vehicle.Velocity;

        return steeringForce;
    }
}
