using UnityEngine;

namespace Assets.Scripts.SteeringBehaviours.AvoidanceBehaviours
{
    /// <summary>
    /// Returns a force that directs the agent away from walls in its path
    /// </summary>
    public class WallAvoidance : AvoidanceBehaviourBase
    {
        [Tooltip("The default length of the line traces."/*At runtime, it will be proportional to the speed of the agent."*/)]
        [SerializeField] private float BaseLineTraceLength = 5.0f;

        [Tooltip("The angle of the side detection whiskers (degrees)")]
        [SerializeField] private float SecondaryTraceAngleOffset = 40.0f;

        public override Vector3 Calculate()
        {
            Vector3 steeringForce = Vector3.zero;

            Vector3 traceStartLocation = transform.position;

            float lineTraceLength = BaseLineTraceLength; /* * VehicleComponent.GetSpeed();*/

            for (int i = -1; i < 2; i++)
            {
                Vector3 traceDirection = transform.forward * lineTraceLength;

                traceDirection = Quaternion.Euler(0, i * SecondaryTraceAngleOffset, 0) * traceDirection;

                Vector3 traceEndLocation = traceStartLocation + traceDirection;

                //Debug.DrawLine(traceStartLocation, traceEndLocation, Color.blue);

                if (Physics.Linecast(traceStartLocation, traceEndLocation, out RaycastHit hit, GlobalSteeringFunctions.WallLayerMask))
                {
                    float penetrationDistance = lineTraceLength - hit.distance;

                    //float forceMultipler = penetrationDistance / hit.distance;

                    steeringForce += hit.normal * penetrationDistance;
                }

            }

            return steeringForce;
        }
    }
}
