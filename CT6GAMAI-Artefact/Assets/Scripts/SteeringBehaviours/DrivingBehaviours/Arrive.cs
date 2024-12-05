using UnityEngine;

/// <summary>
/// Returns a force that directs the agent towards a target position, but aplies a scalar to adjust it's magnitude based on the distance to the target
/// </summary>
public class Arrive : DrivingBehaviourBase
{
    [Tooltip("The location we want to arrive at")]
    [SerializeField] private Vector3 TargetPos;

    [Tooltip("The distance at which we begin to slow down")]
    [SerializeField] private float SlowingDistance = 1.0f;

    public override Vector3 Calculate()
    {
        return GetArrivingForceToLocation(VehicleComponent, transform.position, TargetPos, SlowingDistance);
    }

    /// <summary>
    /// Returns a force that directs the agent towards a target position, but aplies a scalar to adjust it's magnitude based on the distance to the target
    /// </summary>
    /// <param name="TargetPosition">The location we want to arrive at</param>
    /// <param name="SlowingDistance">The distance at which we begin to slow down</param>
    public static Vector3 GetArrivingForceToLocation(Vehicle VehicleComponent, Vector3 CurrentPosition, Vector3 TargetPosition, float SlowingDistance)
    {
        Vector3 toTarget = TargetPosition - CurrentPosition;

        float distance = toTarget.magnitude;

        float speed = VehicleComponent.GetMaxSpeed();

        if (distance < SlowingDistance)
        {
            speed = distance / SlowingDistance;

            speed = Mathf.Clamp(speed, speed, VehicleComponent.GetMaxSpeed());
        }

        Vector3 desiredVelocity = toTarget.normalized * speed;

        Vector3 steeringForce = desiredVelocity - VehicleComponent.GetVelocity();

        return steeringForce;
    }
}
