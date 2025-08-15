using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [Header("Beam")]
    [SerializeField] private float maxLength = 100f;   // total beam length budget
    [SerializeField] private int maxBounces = 8;       // how many reflections
    [SerializeField] private LayerMask hitMask = ~0;   // layers the laser can hit
    [SerializeField] private float surfaceOffset = 0.001f; // avoids self-hit at the surface
    [SerializeField] private float beamDuration = 1f; // avoids self-hit at the surface

    private LineRenderer lr;
    private Coroutine beamCoroutine;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void DrawLaser(Vector3 origin, Vector3 direction)
    {
        // Cancel any existing beam cleanup coroutine
        if (beamCoroutine != null)
            StopCoroutine(beamCoroutine);

        // Actually draw the beam
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

        if (bounces > maxBounces && remaining > 0f)
        {
            Vector3 endPoint = origin + direction * remaining;
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, endPoint);
        }

        // Start the cleanup coroutine
        beamCoroutine = StartCoroutine(ClearBeamAfterDelay());
    }

    private IEnumerator ClearBeamAfterDelay()
    {
        yield return new WaitForSeconds(beamDuration);
        lr.positionCount = 0; // clears the beam
        beamCoroutine = null;
    }
}
