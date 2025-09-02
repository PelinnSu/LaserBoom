using UnityEngine;

public class ParticleProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // auto-destroy after X seconds
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
