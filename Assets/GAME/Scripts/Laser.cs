using System;
using UnityEngine;
using DG.Tweening;

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

    private Target target;
    private Target storedTarget;
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 0.5f;
    [SerializeField] private float projectileDieDuration = 2f;
    [SerializeField] private float projectileDistanceTravelled = 2f;
    [SerializeField] private Transform firePoint;
    private GameObject _currentProjectile;
    
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
        ShootProjectile();

    }

    public void StopFiring()
    {
        isFiring = false;
        lr.positionCount = 0; // clear laser
        target = null; // reset target
        if (target == null)
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
    private void ShootProjectile()
    {
        // spawn projectile at the firePoint’s position & rotation
        _currentProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // move it forward relative to player’s facing direction
        Vector3 targetPos = firePoint.position + firePoint.forward * projectileDistanceTravelled; 
        _currentProjectile.transform.DOMove(targetPos, projectileSpeed);
        Destroy(_currentProjectile, projectileDieDuration);
        
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Projectile collided with: " + other.name);
        if (other.gameObject.GetComponent<AILocomotion>())
        {
            Debug.Log("Projectile hit an enemy!");
        }
    }
}
