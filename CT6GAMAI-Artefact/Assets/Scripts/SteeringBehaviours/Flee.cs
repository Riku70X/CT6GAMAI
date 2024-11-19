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
        Vehicle vehicle = GetComponent<Vehicle>();

        Vector3 desiredVelocity = (transform.position - TargetPos).normalized * vehicle.GetMaxSpeed();

        Vector3 steeringForce = desiredVelocity - vehicle.GetVelocity();

        return steeringForce;
    }
}
