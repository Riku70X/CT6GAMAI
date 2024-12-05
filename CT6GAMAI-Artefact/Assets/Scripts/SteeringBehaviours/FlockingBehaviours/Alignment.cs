using UnityEngine;

/// <summary>
/// Returns a force that steers an agent towards the same heading as its neighbours
/// </summary>
public class Alignment : FlockingBehaviourBase
{
    public override Vector3 Calculate()
    {
        Vector3 steeringForce = Vector3.zero;
        Vector3 averageHeading = Vector3.zero;

        GameObject[] neighbours = GlobalSteeringFunctions.GetAllNearbyAgents(gameObject, VehicleComponent.GetVisionRadius(), VehicleComponent.GetVisionAngle() * Mathf.Deg2Rad);

        if (neighbours.Length != 0)
        {
            foreach (GameObject neighbour in neighbours)
            {
                averageHeading += neighbour.transform.forward;
            }

            averageHeading /= neighbours.Length;

            steeringForce = averageHeading - transform.forward;
        }

        //Debug.Log($"Alignment magnitude is {steeringForce.magnitude}");

        return steeringForce;
    }
}
