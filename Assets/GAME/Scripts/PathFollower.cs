using TMPro;
using UnityEngine;

public class PlayerWaypointCounter : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints; // Assign empty GameObjects in inspector
    [SerializeField] private float reachThreshold = 0.5f; // How close to count as "reached"

    [Header("Lap Settings")]
    [SerializeField] private int lapsToComplete = 3;

    private int currentWaypointIndex = 0;
    private int lapCount = 0;
    [SerializeField] private TextMeshPro textMesh;

    private void Update()
    {
        if (waypoints.Length == 0 || lapCount >= lapsToComplete) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Check if player is close enough to the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < reachThreshold)
        {
            currentWaypointIndex++;
            Debug.Log("Reached waypoint: " + targetWaypoint.name);
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
                lapCount++;
                textMesh.text = "Laps: " + lapCount + "/" + lapsToComplete;
                Debug.Log("Lap finished! Current laps: " + lapCount);

                if (lapCount >= lapsToComplete)
                {
                    Debug.Log("Player completed all laps!");
                }
            }
        }
    }
}
