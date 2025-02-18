using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public Slider xpBar;
    public Text levelText;
    public Image hpBar;
    public Image portraitImage;

    /// <summary>
    /// Call this to update the XP bar (value should be 0–1).
    /// </summary>
    public void UpdateXPBar(float normalizedXP)
    {
        if (xpBar != null)
            xpBar.value = normalizedXP;
    }

    /// <summary>
    /// Update the level text (e.g., "Level 5").
    /// </summary>
    public void UpdateLevel(int level)
    {
        if (levelText != null)
            levelText.text = "Level " + level.ToString();
    }
}
