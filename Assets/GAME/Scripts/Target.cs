using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour
{
    [SerializeField] private float fillDuration = 0.5f; // seconds to reach full fill
    private Material _material;
    private float currentFill = 0f;
    private bool isBeingHit = false;
    void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _material.SetFloat("_Fill", currentFill);
    }

    void Update()
    {
        if (isBeingHit)
        {
            // Progress fill based on time
            currentFill += Time.deltaTime / fillDuration;
            currentFill = Mathf.Clamp01(currentFill);
            _material.SetFloat("_Fill", currentFill);
            if (currentFill >= 1)
            {
                StopFillShader(); // stop filling when full
            }
        }
    }

    public void StartFillShader()
    {
        isBeingHit = true;
    }
    public void StopFillShader()
    {
        isBeingHit = false;
    }
}
