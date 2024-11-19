using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent towards a predicted future location of a target agent
/// </summary>
public class Pursuit : SteeringBehaviourBase
{
    [Tooltip("The agent we want to pursue")]
    [SerializeField] private GameObject Evader;

    public override Vector3 Calculate()
    {
        Vehicle vehicle = GetComponent<Vehicle>();

        if (!Evader.TryGetComponent<Vehicle>(out var evaderVehicle))
        {
            Debug.LogError("Pursuit::Calculate() has failed - evaderVehicle was null. Evader needs a Vehicle component.");

            return Vector3.zero;
        }

        Seek seek = gameObject.AddComponent<Seek>();

        Vector3 toEvader = Evader.transform.position - transform.position;

        float relativeHeading = Vector3.Dot(transform.forward.normalized, Evader.transform.forward.normalized);

        // If facing each other
        if (relativeHeading > 0)
        {
            seek.SetTargetPosition(Evader.transform.position);
        }
        else
        {
            float lookAheadTine = toEvader.magnitude / (vehicle.GetMaxSpeed() + evaderVehicle.GetSpeed());

            Vector3 evaderFuturePosition = Evader.transform.position + evaderVehicle.GetVelocity() * lookAheadTine;

            seek.SetTargetPosition(evaderFuturePosition);
        }

        Vector3 steeringForce = seek.Calculate();

        Destroy(seek);

        return steeringForce;
    }
}
