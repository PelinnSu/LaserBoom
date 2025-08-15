using UnityEngine;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    private TrajectoryPredictor _trajectoryPredictor;

    [SerializeField] Rigidbody objectToThrow;
    [SerializeField] Transform startPosition;
    [SerializeField , Range(0.0f,50.0f)] float throwForce;
}
