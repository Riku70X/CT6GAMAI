using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent towards a predicted future location of a target agent
/// </summary>
public class Persuit : SteeringBehaviourBase
{
    [Tooltip("The agent we want to persue")]
    [SerializeField] private GameObject Evader;

    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();

        if (!Evader.TryGetComponent<Vehicle>(out var evaderVehicle))
        {
            Debug.LogError("Persuit::Calculate() has failed - evaderVehicle was null. Evader needs a Vehicle component.");
        }

        Vector3 toEvader = Evader.transform.position - transform.position;

        float relativeHeading = Vector3.Dot(transform.forward.normalized, Evader.transform.forward.normalized);

        // If facing each other
        if (relativeHeading > 0)
        {
            Seek seek = new Seek(Evader.transform.position);

            return seek.Calculate();
        }

        float lookAheadTine = toEvader.magnitude / (vehicle.GetMaxSpeed() + evaderVehicle.GetSpeed());

        return Vector3.one;
    }
}
