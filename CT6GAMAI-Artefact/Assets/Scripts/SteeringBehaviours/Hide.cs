using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent towards the nearest hiding spot from a target agent
/// </summary>
public class Hide : SteeringBehaviourBase
{
    

    public override Vector3 Calculate()
    { 

        List<Collider> obstacles = ObstacleAvoidance.GetAllTaggedObjectsInBox(ObstacleAvoidance.ObstacleTag, transform.position, new(50, 50, 50));

        throw new System.NotImplementedException();
    }
}
