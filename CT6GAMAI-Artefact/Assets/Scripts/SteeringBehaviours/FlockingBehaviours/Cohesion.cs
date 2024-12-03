using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that steers an agent towards the average position of all its neighbours
/// </summary>
public class Cohesion : FlockingBehaviourBase
{
    public override Vector3 Calculate()
    {
        Vector3 steeringForce = Vector3.zero;
        Vector3 averagePosition = Vector3.zero;

        GameObject[] neighbours = GlobalSteeringFunctions.GetAllNearbyAgents(gameObject, VehicleComponent.GetVisionRadius(), VehicleComponent.GetVisionAngle() * Mathf.Deg2Rad);

        if (neighbours.Length != 0)
        {
            foreach (GameObject neighbour in neighbours)
            {
                averagePosition += neighbour.transform.position;
            }

            averagePosition /= neighbours.Length;

            steeringForce = Seek.GetSeekingForceToLocation(VehicleComponent, transform.position, averagePosition);
        }

        //Debug.Log($"Cohesion magnitude is {steeringForce.magnitude}");

        return steeringForce;
    }
}
