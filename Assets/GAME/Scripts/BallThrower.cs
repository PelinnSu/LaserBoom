using UnityEngine;
using UnityEngine.InputSystem;

public class BallThrower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private CharacterController playerController; // reference to your PlayerController

    [Header("Throw Settings")]
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float destroyDelay = 5f;

    private InputSystem_Actions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Aim.performed += OnShootPerformed;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
        _playerInputActions.Player.Aim.performed -= OnShootPerformed;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        ThrowBall();
    }

    private void ThrowBall()
    {
        GameObject ball = Instantiate(ballPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Base throw direction
            Vector3 throwDirection = throwPoint.forward * throwForce;

            // Add player velocity so throw feels consistent when moving
            Vector3 playerVelocity = playerController != null ? playerController.velocity : Vector3.zero;

            // Apply both throw force + player momentum
            rb.linearVelocity = playerVelocity + throwDirection;
        }

        Destroy(ball, destroyDelay);
    }
}
