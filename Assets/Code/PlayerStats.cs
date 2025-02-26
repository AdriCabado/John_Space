using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    [HideInInspector] public int currentHP;

    [Header("XP Settings")]
    public int currentXP = 0;
    public int xpToNextLevel = 100; // XP required to level up
    public int playerLevel = 1; // Starts at level 1

    [Header("UI References")]
    [Tooltip("Fill Image used for the HP bar (Mega Man style)")]
    public Image hpBarImage;
    [Tooltip("Text component showing HP (optional)")]
    public TextMeshProUGUI hpText;
    [Tooltip("XP bar UI element")]
    public Image xpBarImage;
    [Tooltip("Level text UI element")]
    public TextMeshProUGUI levelText;

    [Header("Armor Modifier")]
    [Tooltip("Multiplier for reducing incoming damage (e.g. from a Titanium passive upgrade)")]
    public float armorModifier = 1f; // Lower value means less damage taken

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthUI();
        UpdateXPUI();
    }

    /// <summary>
    /// Call this method to subtract HP (damage) from the player.
    /// </summary>
    public void TakeDamage(int damage)
    {
        // Apply armor reduction (if any)
        int finalDamage = Mathf.FloorToInt(damage / armorModifier);
        currentHP -= finalDamage;
        if (currentHP < 0) currentHP = 0;
        UpdateHealthUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("John has died!");
        // Add death or game-over logic here.
    }

    void UpdateHealthUI()
    {
        if (hpBarImage != null)
        {
            hpBarImage.fillAmount = (float)currentHP / maxHP;
        }
        if (hpText != null)
        {
            hpText.text = currentHP + " / " + maxHP;
        }
    }

    // XP handling
    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
        UpdateXPUI();
    }

    private void LevelUp()
    {
        playerLevel++;
        currentXP -= xpToNextLevel; // Carry over extra XP
        xpToNextLevel = Mathf.FloorToInt(xpToNextLevel * 1.2f); // Increase next level requirement
        Debug.Log("John leveled up! Now level " + playerLevel);

        // Update UI
        if (levelText != null)
        {
            levelText.text = "Level " + playerLevel;
        }

        UpdateXPUI();
    }

    void UpdateXPUI()
    {
        if (xpBarImage != null)
        {
            xpBarImage.fillAmount = (float)currentXP / xpToNextLevel;
        }
    }

    // Collision handling with asteroids
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            int contactDamage = 10;
            TakeDamage(contactDamage);
            Debug.Log("John hit by asteroid! Took " + contactDamage + " damage.");
        }
    }

    
}
