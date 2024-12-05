using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class containing static functions and variables to be used by Steering Behaviours.
/// Should only exist in one instance in a scene.
/// </summary>
public class GlobalSteeringFunctions : MonoBehaviour
{
    [Tooltip("The name of the Layer used by obstacles. This should match the Obstacle layer name in the project files ('Tags & Layers').")]
    private const string ObstacleLayerName = "Obstacle";

    [Tooltip("The name of the Layer used by walls. This should match the Wall layer name in the project files ('Tags & Layers').")]
    private const string WallLayerName = "Wall";

    [Tooltip("The name of the Layer used by agents. This should match the Agent layer name in the project files ('Tags & Layers').")]
    private const string AgentLayerName = "Agent";

    [Tooltip("The layer mask used by obstacles.")]
    public static int ObstacleLayerMask;

    [Tooltip("The layer mask used by walls.")]
    public static int WallLayerMask;

    [Tooltip("The layer mask used by agents.")]
    public static int AgentLayerMask;

    private void Awake()
    {
        ObstacleLayerMask = 1 << LayerMask.NameToLayer(ObstacleLayerName);

        WallLayerMask = 1 << LayerMask.NameToLayer(WallLayerName);

        AgentLayerMask = 1 << LayerMask.NameToLayer(AgentLayerName);
    }

    /// <summary>
    /// Locates all the nearby agents for flocking purposes.
    /// </summary>
    /// <param name="Agent">The agent we are checking around</param>
    /// <param name="VisionRadius">The radius around the agent we are searching</param>
    /// <param name="VisionAngle">The angle of vision of the agent, in radians (0 to pi/2)</param>
    /// <returns>The array of agents</returns>
    public static GameObject[] GetAllNearbyAgents(GameObject Agent, float VisionRadius, float VisionAngle)
    {
        List<GameObject> agentList = new();

        Collider[] agentColliders = Physics.OverlapSphere(Agent.transform.position, VisionRadius, AgentLayerMask);

        foreach (Collider collider in agentColliders)
        {
            // ignore own collider, check if in sight
            if (collider.gameObject != Agent && IsAgentInSight(Agent, collider.gameObject, VisionAngle))
            {
                agentList.Add(collider.gameObject);
            }
        }

        return agentList.ToArray();
    }

    /// <summary>
    /// Returns true if the other agent lies within the vision cone of the first agent.
    /// </summary>
    /// <param name="Agent">The agent that is looking around</param>
    /// <param name="OtherAgent">The agent we are looking for</param>
    /// <param name="VisionAngle">The angle of vision, in radians (0 to pi/2)</param>
    public static bool IsAgentInSight(GameObject Agent, GameObject OtherAgent, float VisionAngle)
    {
        if (Vector3.Dot(Agent.transform.forward, (OtherAgent.transform.position - Agent.transform.position).normalized) > Mathf.Cos(VisionAngle))
        {
            return true;
        }
        return false;
    }
}
