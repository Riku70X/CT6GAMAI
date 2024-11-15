using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from a target position
/// </summary>
public class Flee : SteeringBehaviourBase
{
    [SerializeField]
    private Vector3 TargetPos;

    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();
        Vector3 DesiredVelocity = (transform.position - TargetPos).normalized * vehicle.MaxSpeed;

        return (DesiredVelocity - vehicle.Velocity);
    }
}
