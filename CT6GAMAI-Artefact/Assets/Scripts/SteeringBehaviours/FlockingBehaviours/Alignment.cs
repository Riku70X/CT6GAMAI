using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that steers an agent towards the same heading as its neighbours
/// </summary>
public class Alignment : SteeringBehaviourBase
{
    public override Vector3 Calculate()
    {
        Vector3 steeringForce = Vector3.zero;
        Vector3 averageHeading = Vector3.zero;

        //GameObject[] neighbours = GlobalSteeringFunctions.GetAllNearbyAgents(gameObject, VisionRadius, VisionAngle * Mathf.Deg2Rad);

        throw new System.NotImplementedException();
    }
}
