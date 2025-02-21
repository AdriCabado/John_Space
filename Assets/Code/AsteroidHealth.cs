using UnityEngine;

public class AsteroidHealth : MonoBehaviour
{
    [Header("Asteroid Health Settings")]
    public float maxHealth = 10f;
    private float currentHealth;

    [Header("Asteroid Death Settings")]
    public Animator animator; // Assign the Animator in the Inspector
    public float destroyDelay = 0.5f; // Delay before destroying the asteroid after animation

    private bool isDestroyed = false; // Prevent multiple destructions

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return; // Ignore damage if already in destruction phase

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

        // Play destroy animation if there's an animator attached
        if (animator != null)
        {
            animator.SetTrigger("Destroy");
        }

        // Destroy asteroid after the animation plays
        Destroy(gameObject, destroyDelay);
    }
}
