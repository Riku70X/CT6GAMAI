using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Applies the forces of the Steering Behaviours attached to this object
/// </summary>
public class Vehicle : MonoBehaviour
{
    //Accessors

    public Vector3 GetVelocity() { return Velocity; }
    public float GetSpeed() { return Velocity.magnitude; }
    public float GetMass() { return Mass; }
    public float GetMaxSpeed() { return MaxSpeed; }
    public float GetMaxForce() { return MaxForce; }
    //public float GetMaxTurnRate() { return MaxTurnRate; }
    public float GetVisionRadius() { return VisionRadius; }
    public float GetVisionAngle() { return VisionAngle; }

    //"Updated" Values

    [Header("Read Only")]

    [Tooltip("This is applied to the current position every frame")]
    [SerializeField] private Vector3 Velocity;

    //Position, Heading and Side can be accessed from the transform component with transform.position, transform.forward and transform.right respectively

    //"Constant" values, they are serialized so we can adjust them through the editor

    [Header("Base Stats")]

    [Tooltip("Represents the weight of an object, will effect its acceleration")]
    [SerializeField] private float Mass = 1;

    [Tooltip("The maximum speed this agent can move per second")]
    [SerializeField] private float MaxSpeed = 3;

    [Tooltip("The thrust this agent can produce")]
    [SerializeField] private float MaxForce = 1;

    //[Tooltip("We use this to determine how fast the agent can turn, but just ignore it for, we won't be using it")]
    //[SerializeField] private float MaxTurnRate = 1.0f;

    [Header("Flocking Stats")]

    [Tooltip("The radius around the agent that we search for neighbours")]
    [SerializeField] private float VisionRadius = 15.0f;

    [Tooltip("The side angle of vision of the agent has (180 degrees -> can see directly behind)")]
    [Range(0.0f, 180.0f)]
    [SerializeField] private float VisionAngle = 135.0f;

    void Update()
    {
        Vector3 steeringForce = Vector3.zero;

        DrivingBehaviourBase[] drivingBehaviours = GetComponents<DrivingBehaviourBase>();
        AvoidanceBehaviourBase[] avoidanceBehaviours = GetComponents<AvoidanceBehaviourBase>();
        FlockingBehaviourBase[] flockingBehaviours = GetComponents<FlockingBehaviourBase>();

        //Prioritise avoidance over driving, and driving over flocking

        foreach (AvoidanceBehaviourBase avoidanceBehaviour in avoidanceBehaviours)
        {
            bool bSucceeded = AccumulateForce(ref steeringForce, avoidanceBehaviour.Calculate());
            if (!bSucceeded) break; // exit the loop if already full
        }

        Debug.Log($"Avoidance running sum {steeringForce.magnitude}");

        foreach (DrivingBehaviourBase drivingBehaviour in drivingBehaviours)
        {
            bool bSucceeded = AccumulateForce(ref steeringForce, drivingBehaviour.Calculate());
            if (!bSucceeded) break; // exit the loop if already full
        }

        Debug.Log($"Driving running sum {steeringForce.magnitude}");

        Vector3 totalFlockingForce = Vector3.zero;

        foreach (FlockingBehaviourBase flockingBehaviour in flockingBehaviours)
        {
            //bool bSucceeded = AccumulateForce(ref steeringForce, flockingBehaviour.Calculate());
            //if (!bSucceeded) break; // exit the loop if already full

            totalFlockingForce += flockingBehaviour.Calculate();
        }

        AccumulateForce(ref steeringForce, totalFlockingForce);

        Debug.Log($"Flocking running sum {steeringForce.magnitude}");

        // ensure
        steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);

        Debug.DrawRay(transform.position, steeringForce, Color.green);

        Vector3 acceleration = steeringForce / Mass;

        Velocity += acceleration * Time.deltaTime;

        Debug.Log($"Desired Speed {Velocity.magnitude}");

        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);

        if (Velocity != Vector3.zero)
        {
            transform.position += Velocity * Time.deltaTime;

            transform.forward = Velocity.normalized;
        }

        //transform.right should update on its own once we update the transform.forward
    }

    /// <summary>
    /// Adds the force to the running sum, up until the maximum force is reached.
    /// </summary>
    /// <param name="RunningSum">The cumulative force we are adding to</param>
    /// <param name="ForceToAdd">The force we are adding</param>
    /// <returns>True if we added a force, false if the sum was already full</returns>
    private bool AccumulateForce(ref Vector3 RunningSum, Vector3 ForceToAdd)
    {
        float MagnitudeSoFar = RunningSum.magnitude;

        float MagnitudeRemaining = MaxForce - MagnitudeSoFar;

        if (MagnitudeRemaining <= 0)
        {
            return false;
        }

        if (ForceToAdd.magnitude <= MagnitudeRemaining)
        {
            RunningSum += ForceToAdd;
        }
        else
        {
            RunningSum += ForceToAdd.normalized * MagnitudeRemaining;
        }

        return true;
    }
}
