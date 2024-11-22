using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent towards the nearest hiding spot from a target agent
/// </summary>
public class Hide : SteeringBehaviourBase
{
    [Tooltip("The agent we want to hide from")]
    [SerializeField] private GameObject Enemy;

    [Tooltip("The radius around the agent where it will search for hiding spots")]
    [SerializeField] private float SearchRadius = 50.0f;

    [Tooltip("The distance away from an obstacle's edge where an agent will hide")]
    [SerializeField] private float DistanceFromObstacle = 10.0f;

    // Obstacle Layer logic taken from ObstacleAvoidance.cs... maybe this could be stored elsewhere?
    [Tooltip("The name of the Layer used by obstacles. This should match the Obstacle layer name in the project files ('Tags & Layers').")]
    private readonly string ObstacleLayerName = "Obstacle";

    [Tooltip("The layer mask used by obstacles.")]
    private int ObstacleLayerMask;

    protected override void Awake()
    {
        base.Awake();

        ObstacleLayerMask = 1 << LayerMask.NameToLayer(ObstacleLayerName);
    }

    public override Vector3 Calculate()
    {
        //expensive to call every frame...
        Collider[] obstacles = Physics.OverlapSphere(transform.position, SearchRadius, ObstacleLayerMask);

        foreach (Collider obstacle in obstacles)
        {
            float distanceAway = obstacle.bounds.extents.magnitude + DistanceFromObstacle;

            Vector3 enemyToObstacle = (obstacle.transform.position - Enemy.transform.position).normalized;

            Vector3 hidingSpot = (enemyToObstacle * distanceAway) + obstacle.transform.position;
        }

        throw new System.NotImplementedException();
    }
}
