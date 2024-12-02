using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that steers an agent away from the agents in its neighbourhood region
/// </summary>
public class Separation : SteeringBehaviourBase
{
    [Tooltip("The radius around the agent that we search for neighbours")]
    [SerializeField] private float VisionRadius = 15.0f;

    [Tooltip("The side angle of vision of the agent has (180 degrees -> can see directly behind)")]
    [Range(0.0f, 180.0f)]
    [SerializeField] private float VisionAngle = 135.0f;

    public override Vector3 Calculate()
    {
        Vector3 steeringForce = Vector3.zero;

        GameObject[] neighbours = GlobalSteeringFunctions.GetAllNearbyAgents(gameObject, VisionRadius, VisionAngle * Mathf.Deg2Rad);

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

        return steeringForce;
    }
}
