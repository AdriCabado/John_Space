using UnityEngine;

public class ElectricFieldPrefab : MonoBehaviour
{
    [Tooltip("Damage applied to asteroids on contact.")]
    public float damage = 9999f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Electric Field hit an asteroid!");
            // Attempt to destroy the asteroid by applying full damage.
            AsteroidHealth asteroid = other.GetComponent<AsteroidHealth>();
            if (asteroid != null)
            {
                asteroid.TakeDamage(damage);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
