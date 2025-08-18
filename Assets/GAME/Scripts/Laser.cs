using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [Header("Beam")]
    [SerializeField] private float maxLength = 100f;
    [SerializeField] private int maxBounces = 8;
    [SerializeField] private LayerMask hitMask = ~0;
    [SerializeField] private float surfaceOffset = 0.001f;
    private LineRenderer lr;
    private bool isFiring = false;
    [SerializeField] private Target target;
    [SerializeField] private Target storedTarget;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0; // hide initially
    }

    void Update()
    {
        if (isFiring)
        {
            DrawLaser(transform.position, transform.forward);
        }
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
        lr.positionCount = 0; // clear laser
        target = null; // reset target
        if(target==null)
            storedTarget.StopFillShader();
    }

    private void DrawLaser(Vector3 origin, Vector3 direction)
    {
        lr.positionCount = 1;
        lr.SetPosition(0, origin);

        float remaining = maxLength;
        int bounces = 0;

        while (remaining > 0f && bounces <= maxBounces)
        {
            if (Physics.Raycast(origin, direction, out RaycastHit hit, remaining, hitMask, QueryTriggerInteraction.Ignore))
            {
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, hit.point);

                target = hit.collider.GetComponent<Target>();
                if (target != null)
                {
                    target.StartFillShader(); // start filling shader on hit
                    storedTarget = target; // store the target to call the target in StopFiring
                }
              
                remaining -= hit.distance;
                direction = Vector3.Reflect(direction, hit.normal).normalized;
                origin = hit.point + direction * surfaceOffset;
                bounces++;
                continue;
            }

            Vector3 endPoint = origin + direction * remaining;
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, endPoint);
            break;
        }
    }
}
