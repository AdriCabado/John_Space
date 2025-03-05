using UnityEngine;

public class RocketPrefab : MonoBehaviour
{
    [Tooltip("Time in seconds before the rocket is automatically destroyed (if no collision occurs).")]
    public float lifetime = 5f;

    [Tooltip("Damage dealt by the rocket's explosion.")]
    public float damage = 30f;

    [Tooltip("Explosion effect prefab to instantiate on impact.")]
    public GameObject explosionPrefab;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the rocket hits an asteroid, apply damage.
        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Rocket hit asteroid!");
            AsteroidHealth asteroid = other.GetComponent<AsteroidHealth>();
            if (asteroid != null)
            {
                asteroid.TakeDamage(damage);
            }
        }

        // Instantiate an explosion effect if assigned.
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the rocket upon collision.
        Destroy(gameObject);
    }  
}
