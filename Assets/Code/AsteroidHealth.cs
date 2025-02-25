using UnityEngine;

public class AsteroidHealth : MonoBehaviour
{
    [Header("Asteroid Health Settings")]
    public float maxHealth = 10f;
    private float currentHealth;

    [Header("Asteroid Death Settings")]
    private Animator animator; // Automatically gets assigned in Start()
    public float destroyDelay = 0.5f; // Delay before destruction

    private bool isDestroyed = false; // Prevent multiple destructions

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Automatically gets the Animator component
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return; // Ignore further damage once destroyed

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDestroyed) return; // Ensure it only triggers once
        isDestroyed = true;

        // Play destroy animation if an Animator is attached
        if (animator != null)
        {
            animator.SetTrigger("Destroy");
        }

        // Disable the collider to prevent further collisions
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Disable the rigidbody to stop any physics interactions
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }

        // Destroy the asteroid after the animation delay
        Destroy(gameObject, destroyDelay);
    }
}
