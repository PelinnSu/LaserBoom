//using UnityEngine;
//using UnityEngine.InputSystem;

//[RequireComponent(typeof(TrajectoryPredictor))]
//public class ProjectileThrow : MonoBehaviour
//{
//    [Header("References")]
//    [SerializeField] private Rigidbody objectToThrow;
//    [SerializeField] private float force = 10f;

//    private TrajectoryPredictor trajectoryPredictor;
//    public InputAction fire;

//    private void OnEnable()
//    {
//        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
//        fire.Enable();
//        fire.performed += ThrowObject;
//    }

//    private void OnDisable()
//    {
//        fire.performed -= ThrowObject;
//        fire.Disable();
//    }

//    private void Update()
//    {
//        PredictTrajectory();
//    }

//    /// <summary>
//    /// Predicts the trajectory using mouse input
//    /// </summary>
//    private void PredictTrajectory()
//    {
//        trajectoryPredictor.PredictTrajectory(ProjectileData());
//    }

//    /// <summary>
//    /// Create projectile properties for prediction
//    /// </summary>
//    private ProjectileProperties ProjectileData()
//    {
//        Vector3 direction = GetMouseAimDirection();

//        return new ProjectileProperties
//        {
//            direction = direction,
//            initialPosition = transform.position,
//            initialSpeed = force,
//            mass = objectToThrow.mass,
//            drag = objectToThrow.linearDamping
//        };
//    }

//    /// <summary>
//    /// Get direction from this GameObject to mouse position in world space
//    /// </summary>
//    private Vector3 GetMouseAimDirection()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
//        Plane plane = new Plane(Vector3.up, transform.position); // horizontal plane at object height

//        if (plane.Raycast(ray, out float distance))
//        {
//            Vector3 worldPoint = ray.GetPoint(distance);
//            Vector3 direction = (worldPoint - transform.position).normalized;
//            return direction;
//        }

//        return transform.forward; // fallback
//    }

//    /// <summary>
//    /// Throws the projectile along the predicted direction
//    /// </summary>
//    private void ThrowObject(InputAction.CallbackContext ctx)
//    {
//        Vector3 direction = GetMouseAimDirection();
//        Rigidbody thrownObject = Instantiate(objectToThrow, transform.position, Quaternion.LookRotation(direction));
//        thrownObject.AddForce(direction * force, ForceMode.Impulse);
//    }
//}
