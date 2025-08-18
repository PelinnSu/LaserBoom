using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserController : MonoBehaviour
{
    private InputSystem_Actions _playerInputActions;
    [SerializeField] private Laser laser;

    private void Awake()
    {
        _playerInputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Shoot.performed += OnShootPerformed;
        _playerInputActions.Player.Shoot.canceled += OnShootCanceled;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
        _playerInputActions.Player.Shoot.performed -= OnShootPerformed;
        _playerInputActions.Player.Shoot.canceled -= OnShootCanceled;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        laser.StartFiring(); // starts continuous laser
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        laser.StopFiring(); // stops laser when button released
    }
}
