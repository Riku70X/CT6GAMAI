using Assets.Scripts.ActorComponents;
using UnityEngine;

namespace Assets.Scripts.SteeringBehaviours.DrivingBehaviours
{
    /// <summary>
    /// Returns a force that directs the agent towards a predicted future location of a target agent
    /// </summary>
    public class Pursuit : DrivingBehaviourBase
    {
        [Tooltip("The agent we want to pursue")]
        [SerializeField] private GameObject Evader;

        public override Vector3 Calculate()
        {
            if (!Evader.TryGetComponent<VehicleComponent>(out var evaderVehicleComponent))
            {
                Debug.LogError("Pursuit::Calculate() has failed - evaderVehicleComponent was null. Evader needs a Vehicle component.");

                return Vector3.zero;
            }

            Vector3 toEvader = Evader.transform.position - transform.position;

            float relativeHeading = Vector3.Dot(transform.forward.normalized, Evader.transform.forward.normalized);

            Vector3 steeringForce;

            // If facing each other
            if (relativeHeading > 0)
            {
                steeringForce = Seek.GetSeekingForceToLocation(VehicleComponent, transform.position, Evader.transform.position);
            }
            else
            {
                float lookAheadTine = toEvader.magnitude / (VehicleComponent.GetMaxSpeed() + evaderVehicleComponent.GetSpeed());

                Vector3 evaderFuturePosition = Evader.transform.position + evaderVehicleComponent.GetVelocity() * lookAheadTine;

                steeringForce = Seek.GetSeekingForceToLocation(VehicleComponent, transform.position, evaderFuturePosition);
            }

            return steeringForce;
        }
    }
}
