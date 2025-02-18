using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    [HideInInspector] public int currentHP;

    [Header("UI References")]
    [Tooltip("Fill Image used for the HP bar (Mega Man style)")]
    public Image hpBarImage;
    [Tooltip("Text component showing HP (optional)")]
    public Text hpText;
    [Tooltip("Portrait image (set this to your Kingdom Hearts–style portrait)")]
    public Image portraitImage;

    [Header("Armor Modifier")]
    [Tooltip("Multiplier for reducing incoming damage (e.g. from a Titanium passive upgrade)")]
    public float armorModifier = 1f; // lower value means less damage taken

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthUI();
    }

    /// <summary>
    /// Call this to subtract HP (damage) from the player.
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
}
