using UnityEngine;

namespace Assets.Scripts.SteeringBehaviours.FlockingBehaviours
{
    /// <summary>
    /// Returns a force that steers an agent away from the agents in its neighbourhood region
    /// </summary>
    public class Separation : FlockingBehaviourBase
    {
        public override Vector3 Calculate()
        {
            Vector3 steeringForce = Vector3.zero;

            GameObject[] neighbours = GlobalSteeringFunctions.GetAllNearbyAgents(gameObject, VehicleComponent.GetVisionRadius(), VehicleComponent.GetVisionAngle() * Mathf.Deg2Rad);

            foreach (GameObject neighbour in neighbours)
            {
                Vector3 toOurAgent = transform.position - neighbour.transform.position;
                float distance = toOurAgent.magnitude;

                toOurAgent = toOurAgent.normalized;

                if (distance != 0.0f)
                {
                    toOurAgent /= distance;
                }

                steeringForce += toOurAgent;
            }

            //Debug.Log($"Separation magnitude is {steeringForce.magnitude}");

            return steeringForce;
        }
    }
}
