using UnityEngine;
using UnityEngine.AI;
public class AILocomotion : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float maxTime = 1.0f; // limit finding path
    [SerializeField] private float maxDistance = 1.0f;
    private float _timer = 0.0f;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0.0f)
        {
            float sqrDistance = (playerTransform.position - _agent.destination).sqrMagnitude; // check how far is the player from the destination. if player is not moving no need to find a path
            if (sqrDistance > maxDistance * maxDistance)
            {
                _agent.destination = playerTransform.position;
            }

            _timer = maxTime;
        }
        _agent.destination = playerTransform.position;
    }
}
