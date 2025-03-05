using UnityEngine;

public class ExplosionPrefab : MonoBehaviour
{
    [Tooltip("Time in seconds before the explosion is automatically destroyed.")]
    public float lifetime = 0.5f;

    [Tooltip("Damage dealt by this explosion.")]
    public float damage = 3f;

    void Start()
    {
        // Destroy the bullet after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    // This example uses collision detection. If your bullet uses a trigger, replace with OnTriggerEnter2D.
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Asteroid"))
    {
        Debug.Log("Explosion hit asteroid!");
        AsteroidHealth asteroid = other.GetComponent<AsteroidHealth>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(damage);
        }
    }
}
}
