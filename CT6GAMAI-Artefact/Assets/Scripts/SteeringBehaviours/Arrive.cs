using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent towards a target position, but aplies a scalar to adjust it's magnitude based on the distance to the target
/// </summary>
public class Arrive : SteeringBehaviourBase
{
    [Tooltip("The location we want to arrive at")]
    [SerializeField] private Vector3 TargetPos;

    [Tooltip("The distance at which we begin to slow down")]
    [SerializeField] private float SlowingDistance;

    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();

        Vector3 toTarget = TargetPos - transform.position;

        float distance = toTarget.magnitude;

        float speed = vehicle.GetMaxSpeed();

        if (distance < SlowingDistance)
        {
            speed = distance / SlowingDistance;

            speed = Mathf.Clamp(speed, speed, vehicle.GetMaxSpeed());
        }

        Vector3 desiredVelocity = toTarget.normalized * speed;

        Vector3 steeringForce = desiredVelocity - vehicle.GetVelocity();

        return steeringForce;
    }
}
