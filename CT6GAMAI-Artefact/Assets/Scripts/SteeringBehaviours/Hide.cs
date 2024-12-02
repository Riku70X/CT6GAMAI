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
    [SerializeField] private float DistanceFromObstacle = 1.0f;

    [Tooltip("The distance from the hiding spot where we begin to slow down")]
    [SerializeField] private float SlowingDistance = 1.0f;

    public override Vector3 Calculate()
    {
        //expensive to call every frame...
        Collider[] obstacles = Physics.OverlapSphere(transform.position, SearchRadius, GlobalSteeringFunctions.ObstacleLayerMask);

        Vector3 bestHidingSpot = Vector3.zero;

        foreach (Collider obstacle in obstacles)
        {
            float distanceAway = obstacle.bounds.extents.magnitude + DistanceFromObstacle;

            Vector3 enemyToObstacle = (obstacle.transform.position - Enemy.transform.position).normalized;

            Vector3 hidingSpot = (enemyToObstacle * distanceAway) + obstacle.transform.position;

            if (Vector3.Distance(transform.position, hidingSpot) < Vector3.Distance(transform.position, bestHidingSpot))
            {
                bestHidingSpot = hidingSpot;
            }
        }

        Vector3 steeringForce;

        // No hiding spot was found
        if (bestHidingSpot == Vector3.zero) 
        {
            steeringForce = Evade.GetEvadingForceFromAgent(VehicleComponent, transform.position, Enemy);
        }
        else
        {
            steeringForce = Arrive.GetArrivingForceToLocation(VehicleComponent, transform.position, bestHidingSpot, SlowingDistance);
        }

        return steeringForce;
    }
}
