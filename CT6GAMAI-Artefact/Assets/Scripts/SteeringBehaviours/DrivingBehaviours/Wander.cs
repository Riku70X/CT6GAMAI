using UnityEngine;

namespace Assets.Scripts.SteeringBehaviours.DrivingBehaviours
{
    public class Wander : DrivingBehaviourBase
    {
        [Tooltip("This is the radius of our constraining circle/sphere")]
        [SerializeField] private float WanderRadius = 10f;

        [Tooltip("This is the distance in front of the agent we move the circle")]
        [SerializeField] private float WanderDistance = 20f;

        [Tooltip("This is the amount of random displacement we can have in a single second")]
        [SerializeField] private float WanderJitter = 1f;

        [Tooltip("This is the angle which represents the point on the circle")]
        float WanderAngle = 0.0f;

        [Tooltip("This is the actual target position; we initialise this to some random value within our WanderRadius at the start using WanderAngle")]
        Vector3 WanderTarget = Vector3.zero;

        protected override void Awake()
        {
            base.Awake();

            WanderAngle = Random.Range(0.0f, Mathf.PI * 2);
            WanderTarget = new Vector3(Mathf.Cos(WanderAngle), 0, Mathf.Sin(WanderAngle)) * WanderRadius;
        }

        public override Vector3 Calculate()
        {
            WanderAngle += Random.Range(-WanderJitter, WanderJitter) * Time.deltaTime;
            WanderTarget = new Vector3(Mathf.Cos(WanderAngle), 0, Mathf.Sin(WanderAngle)) * WanderRadius;

            Vector3 targetWorldPosition = transform.position + WanderTarget;

            targetWorldPosition += transform.forward * WanderDistance;

            Vector3 steeringForce = targetWorldPosition - transform.position;

            return steeringForce;
        }
    }
}
