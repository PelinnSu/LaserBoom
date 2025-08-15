using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
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
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
        _playerInputActions.Player.Shoot.performed -= OnShootPerformed;
    }
    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        laser.DrawLaser(transform.position, transform.forward);
    }
}
