using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    //"Updated" Values

    [Tooltip("This is applied to the current position every frame")]
    public Vector3 Velocity;

    //Position, Heading and Side can be accessed from the transform component with transform.position, transform.forward and transform.right respectively

    //"Constant" values, they are serialized so we can adjust them through the editor

    [Tooltip("Represents the weight of an object, will effect its acceleration")] [SerializeField]
    private float Mass = 1;

    [Tooltip("The maximum speed this agent can move per second")] [SerializeField]
    private float MaxSpeed = 1;

    [Tooltip("The thrust this agent can produce")] [SerializeField]
    private float MaxForce = 1;

    [Tooltip("We use this to determine how fast the agent can turn, but just ignore it for, we won't be using it")] [SerializeField]
    private float MaxTurnRate = 1.0f;

    void Update()
    {
        Vector3 SteeringForce = Vector3.zero;

        //Get all steering behaviours attached to this object and add their calculated steering force onto this SteeringForce
        SteeringBehaviourBase[] steeringBehaviours = GetComponents<SteeringBehaviourBase>();
        foreach (SteeringBehaviourBase steeringBehaviour in steeringBehaviours)
        {
            SteeringForce += steeringBehaviour.Calculate();
        }

        Vector3 Acceleration = SteeringForce / Mass;

        Velocity += Acceleration * Time.deltaTime;

        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);

        if (Velocity != Vector3.zero)
        {
            transform.position += Velocity * Time.deltaTime;

            transform.forward = Velocity.normalized;
        }

        //transform.right should update on its own once we update the transform.forward
    }
}
