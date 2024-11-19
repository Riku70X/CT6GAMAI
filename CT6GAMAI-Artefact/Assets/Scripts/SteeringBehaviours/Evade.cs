using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from a predicted future location of a target agent
/// </summary>
public class Evade : SteeringBehaviourBase
{
    [Tooltip("The agent we want to evade")]
    [SerializeField] private GameObject Pursuer;

    public override Vector3 Calculate()
    {
        if (!Pursuer.TryGetComponent<Vehicle>(out var pursuerVehicle))
        {
            Debug.LogError("Evade::Calculate() has failed - pursuerVehicle was null. Pursuer needs a Vehicle component.");

            return Vector3.zero;
        }

        Flee flee = gameObject.AddComponent<Flee>();

        Vector3 toPursuer = Pursuer.transform.position - transform.position;
        
        float lookAheadTine = toPursuer.magnitude / (VehicleComponent.GetMaxSpeed() + pursuerVehicle.GetSpeed());

        Vector3 pursuerFuturePosition = Pursuer.transform.position + pursuerVehicle.GetVelocity() * lookAheadTine;

        flee.SetTargetPosition(pursuerFuturePosition);

        Vector3 steeringForce = flee.Calculate();

        Destroy(flee);

        return steeringForce;
    }
}
