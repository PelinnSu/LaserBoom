using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        PlayerController.OnPlayerMoving += HandlePlayerMoving;
        PlayerController.OnPlayerStop += HandlePlayerStop;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerMoving -= HandlePlayerMoving;
        PlayerController.OnPlayerStop -= HandlePlayerStop;
    }
    private void HandlePlayerMoving()
    {
        animator.SetBool("isWalk", true);
    }
    private void HandlePlayerStop()
    {
        animator.SetBool("isWalk", false);
    }

}
