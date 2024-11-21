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

    //"Updated" Values

    [Tooltip("This is applied to the current position every frame")]
    [SerializeField] private Vector3 Velocity;

    //Position, Heading and Side can be accessed from the transform component with transform.position, transform.forward and transform.right respectively

    //"Constant" values, they are serialized so we can adjust them through the editor

    [Tooltip("Represents the weight of an object, will effect its acceleration")]
    [SerializeField] private float Mass = 1;

    [Tooltip("The maximum speed this agent can move per second")]
    [SerializeField] private float MaxSpeed = 3;

    [Tooltip("The thrust this agent can produce")]
    [SerializeField] private float MaxForce = 1;

    //[Tooltip("We use this to determine how fast the agent can turn, but just ignore it for, we won't be using it")]
    //[SerializeField] private float MaxTurnRate = 1.0f;

    void Update()
    {
        Vector3 steeringForce = Vector3.zero;

        //Get all steering behaviours attached to this object and add their calculated steering force onto this SteeringForce
        SteeringBehaviourBase[] steeringBehaviours = GetComponents<SteeringBehaviourBase>();
        foreach (SteeringBehaviourBase steeringBehaviour in steeringBehaviours)
        {
            steeringForce += steeringBehaviour.Calculate();
        }

        steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);

        Debug.DrawRay(transform.position, steeringForce, Color.green);

        Vector3 acceleration = steeringForce / Mass;

        Velocity += acceleration * Time.deltaTime;

        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);

        if (Velocity != Vector3.zero)
        {
            transform.position += Velocity * Time.deltaTime;

            transform.forward = Velocity.normalized;
        }

        //transform.right should update on its own once we update the transform.forward
    }
}
