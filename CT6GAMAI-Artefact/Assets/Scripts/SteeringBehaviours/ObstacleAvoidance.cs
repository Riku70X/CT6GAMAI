using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Returns a force that directs the agent away from obstacles in its path
/// </summary>
public class ObstacleAvoidance : SteeringBehaviourBase
{
    [Tooltip("The default length of the detection box. At runtime, it will be proportional to the speed of the agent. Measured from the centre.")]
    [SerializeField] private float BaseDetectionBoxLength = 1.0f;

    [Tooltip("The width of the detection box, equal to the width of the agent's collider. Measured from the centre.")]
    private float DetectionBoxWidth = 0.0f;

    [Tooltip("The height of the detection box, equal to the height of the agent's collider. Measured from the centre.")]
    private float DetectionBoxHeight = 0.0f;

    [Tooltip("Reference to this gameObject's collider component")]
    private Collider ColliderComponent;

    [Tooltip("The name of the Layer used by obstacles. This should match the Obstacle layer name in the project files ('Tags & Layers').")]
    private readonly string ObstacleLayerName = "Obstacle";

    [Tooltip("The layer mask used by obstacles.")]
    private int ObstacleLayerMask;

    //TODO: Move this variable somewhere else
    [Tooltip("ObstacleAvoidance will cause agents to avoid Game Objects tagged with this")]
    public readonly static string ObstacleTag = "Obstacle";

    protected override void Awake()
    {
        base.Awake();

        ObstacleLayerMask = 1 << LayerMask.NameToLayer(ObstacleLayerName);

        if (TryGetComponent(out ColliderComponent))
        {
            DetectionBoxWidth = ColliderComponent.bounds.extents.x;
            DetectionBoxHeight = ColliderComponent.bounds.extents.y;
        }
        else
        {
            Debug.LogError("ObstacleAvoidance::Awake() has failed - gameObject needs to have a collider component");
        }
    }

    public override Vector3 Calculate()
    {
        float detectionBoxLength = BaseDetectionBoxLength * VehicleComponent.GetSpeed();

        Vector3 detectionBoxExtents = new(DetectionBoxWidth, DetectionBoxHeight, detectionBoxLength);
        Vector3 worldDetectionBoxCentre = transform.forward * detectionBoxLength;
        Quaternion detectionBoxRotation = Quaternion.LookRotation(transform.forward, transform.up);

        //GetAllTaggedObjectsInBox(ObstacleTag, transform.position + worldDetectionBoxCentre, detectionBoxExtents, detectionBoxRotation);

        Collider[] obstacles = Physics.OverlapBox(transform.position + worldDetectionBoxCentre, detectionBoxExtents, detectionBoxRotation, ObstacleLayerMask);

        if (obstacles.Length > 0)
        {
            Collider closestObstacle = obstacles[0];
            float shortestDistance = Vector3.Distance(transform.position, closestObstacle.ClosestPoint(transform.position));

            foreach (Collider obstacle in obstacles)
            {
                if (obstacle != ColliderComponent) //ignore own collider
                {
                    float distance = Vector3.Distance(transform.position, obstacle.ClosestPoint(transform.position));
                    if (distance < shortestDistance)
                    {
                        closestObstacle = obstacle;
                        shortestDistance = distance;
                    }
                }
            }

            if (closestObstacle == ColliderComponent) return Vector3.zero; //return if only obstacle found is own collider 

            Vector3 toObstacle = closestObstacle.transform.position - transform.position;

            if (detectionBoxLength == 0.0f) return Vector3.zero; //shouldn't be possible, but here to prevent a NaN

            float forceMultiplier = 1 + (detectionBoxLength - shortestDistance) / detectionBoxLength;

            forceMultiplier *= Vector3.Dot(VehicleComponent.GetVelocity(), toObstacle);

            Vector3 steeringForce = transform.right * forceMultiplier;

            if (Vector3.Dot(toObstacle, transform.right) > 0)
            {
                steeringForce *= -1;
            }

            return steeringForce;
        }

        return Vector3.zero;
    }


    //TODO: Move this function somewhere else
    /// <summary>
    /// Returns a List of every collider within the box with the passed in tag
    /// </summary>
    /// <param name="Tag">The tag of the objects you want to collect</param>
    /// <param name="CentrePoint">The location of the centre of the box (World Space)</param>
    /// <param name="Extents">The extents of the box from the centre (e.g. half the width, half the height, etc.)</param>
    /// <param name="Orientation">The rotation of the box about its centre. Quaternion.LookRotation() is useful for calculating this.</param>
    /// <returns>List of colliders</returns>
    public static List<Collider> GetAllTaggedObjectsInBox(string Tag, Vector3 CentrePoint, Vector3 Extents, [Optional] Quaternion Orientation)
    {
        Collider[] overlappingColliders = Physics.OverlapBox(CentrePoint, Extents, Orientation);

        List<Collider> taggedObjects = new();
        foreach (Collider collider in overlappingColliders)
        {
            if (collider.CompareTag(Tag))
            {
                taggedObjects.Add(collider);
            }
        }

        return taggedObjects;
    }

    //TODO: Move this function somewhere else
    /// <summary>
    /// Returns a List of every collider within the box with the passed in tag
    /// </summary>
    /// <param name="Tag">The tag of the objects you want to collect</param>
    /// <param name="CentrePoint">The location of the centre of the box (World Space)</param>
    /// <param name="Extents">The extents of the box from the centre (e.g. half the width, half the height, etc.)</param>
    /// <param name="Orientation">The rotation of the box about its centre. Quaternion.LookRotation() is useful for calculating this.</param>
    /// <returns>List of colliders</returns>
    //public static List<Collider> GetAllTaggedObjectsInSphere(string Tag, Vector3 CentrePoint, Vector3 Extents, [Optional] Quaternion Orientation)
    //{
    //    Collider[] overlappingColliders = Physics.OverlapSphere()

    //    List<Collider> taggedObjects = new();
    //    foreach (Collider collider in overlappingColliders)
    //    {
    //        if (collider.CompareTag(Tag))
    //        {
    //            taggedObjects.Add(collider);
    //        }
    //    }

    //    return taggedObjects;
    //}

    //private void OnDrawGizmos()
    //{
    //    if (!Application.isPlaying) return;

    //    float detectionBoxLength = BaseDetectionBoxLength * VehicleComponent.GetSpeed();

    //    Vector3 detectionBoxExtents = new(DetectionBoxWidth, DetectionBoxHeight, detectionBoxLength);

    //    Vector3 localDetectionBoxCentre = new(0.0f, 0.0f, detectionBoxLength);

    //    Gizmos.matrix = transform.localToWorldMatrix;
    //    Gizmos.DrawWireCube(localDetectionBoxCentre, detectionBoxExtents * 2);
    //}
}
