using UnityEngine;
using System.Collections;

public class ShieldPrefab : MonoBehaviour
{
    [Tooltip("Explosion effect prefab to instantiate when an asteroid is destroyed (optional).")]
    public GameObject explosionPrefab;

    [Tooltip("Time in seconds before the shield reappears after destroying one asteroid.")]
    public float reappearDelay = 10f;

    private bool hasDestroyedAsteroid = false;

    // Cache the Collider2D and Renderer components.
    private Collider2D shieldCollider;
    private Renderer shieldRenderer;

    void Awake()
    {
        shieldCollider = GetComponent<Collider2D>();
        shieldRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasDestroyedAsteroid)
            return;

        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Shield hit an asteroid! Destroying it and deactivating shield.");

            hasDestroyedAsteroid = true;

            // Destroy the asteroid (apply full damage if it has AsteroidHealth).
            AsteroidHealth asteroid = other.GetComponent<AsteroidHealth>();
            if (asteroid != null)
            {
                asteroid.TakeDamage(asteroid.maxHealth);
            }
            else
            {
                Destroy(other.gameObject);
            }

            // Optionally instantiate an explosion effect.
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            }

            // Instead of disabling the entire GameObject, disable the visual and collision components.
            if (shieldRenderer != null)
                shieldRenderer.enabled = false;
            if (shieldCollider != null)
                shieldCollider.enabled = false;

            // Start the coroutine to re-enable the shield after a delay.
            StartCoroutine(ReenableShieldAfterDelay());
        }
    }

    private IEnumerator ReenableShieldAfterDelay()
    {
        yield return new WaitForSeconds(reappearDelay);

        if (shieldRenderer != null)
            shieldRenderer.enabled = true;
        if (shieldCollider != null)
            shieldCollider.enabled = true;

        hasDestroyedAsteroid = false;
    }
}
