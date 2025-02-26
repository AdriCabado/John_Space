using UnityEngine;

public class AsteroidHealth : MonoBehaviour
{
    [Header("Asteroid Health Settings")]
    public float maxHealth = 10f;
    private float currentHealth;

    [Header("XP Reward")]
    public int xpReward = 10; // Amount of XP given when destroyed

    [Header("Asteroid Death Settings")]
    private Animator animator;
    public float destroyDelay = 0.5f;

    private bool isDestroyed = false;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        // Find player and give XP
        PlayerStats player = FindObjectOfType<PlayerStats>();
        if (player != null)
        {
            player.GainXP(xpReward);
        }

        // Play destroy animation
        if (animator != null)
        {
            animator.SetTrigger("Destroy");
        }

        // Disable physics
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }

        // Destroy the object
        Destroy(gameObject, destroyDelay);
    }
}
