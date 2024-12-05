using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from a predicted future location of a target agent
/// </summary>
public class Evade : DrivingBehaviourBase
{
    [Tooltip("The agent we want to evade")]
    [SerializeField] private GameObject Pursuer;

    public override Vector3 Calculate()
    {
        return GetEvadingForceFromAgent(VehicleComponent, transform.position, Pursuer);
    }

    /// <summary>
    /// Returns a force that directs the agent away from a predicted future location of a target agent
    /// </summary>
    /// <param name="Pursuer">The agent we are evading</param>
    public static Vector3 GetEvadingForceFromAgent(Vehicle VehicleComponent, Vector3 CurrentPosition, GameObject Pursuer)
    {
        if (!Pursuer.TryGetComponent<Vehicle>(out var pursuerVehicle))
        {
            Debug.LogError("Evade::GetEvadingForceFromAgent() has failed - pursuerVehicle was null. Pursuer needs a Vehicle component.");

            return Vector3.zero;
        }

        Vector3 toPursuer = Pursuer.transform.position - CurrentPosition;

        float lookAheadTime = toPursuer.magnitude / (VehicleComponent.GetMaxSpeed() + pursuerVehicle.GetSpeed());

        Vector3 pursuerFuturePosition = Pursuer.transform.position + pursuerVehicle.GetVelocity() * lookAheadTime;

        Vector3 steeringForce = Flee.GetFleeingForceFromLocation(VehicleComponent, CurrentPosition, pursuerFuturePosition);

        return steeringForce;
    }
}
