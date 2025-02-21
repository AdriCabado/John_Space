using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [Tooltip("Time in seconds before the bullet is automatically destroyed.")]
    public float lifetime = 5f;

    [Tooltip("Damage dealt by this bullet.")]
    public float damage = 20f;

    void Start()
    {
        // Destroy the bullet after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    // This example uses collision detection. If your bullet uses a trigger, replace with OnTriggerEnter2D.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet hit an object tagged as "Asteroid" (or any other relevant tag)
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Optionally, you can access a component on the asteroid to apply damage:
            // Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            // if (asteroid != null)
            // {
            //     asteroid.TakeDamage(damage);
            // }
        }
        
        // Destroy the bullet on collision with any object.
        Destroy(gameObject);
    }
}
