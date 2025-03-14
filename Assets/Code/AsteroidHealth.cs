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

    private Collider2D collider2D;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
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

    public void Die()
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
        if (collider2D != null)
        {
            collider2D.enabled = false;
        }

        if (rigidbody2D != null)
        {
            rigidbody2D.isKinematic = true;
        }

        // Destroy the object
        Destroy(gameObject, destroyDelay);
    }
}
