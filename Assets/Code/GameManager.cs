using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Timer")]
    [Tooltip("Game duration in seconds (600 = 10 minutes)")]
    public float gameDuration = 600f;
    private float timer;

    [Tooltip("UI Text to display the timer (mm:ss)")]
    public Text timerText;

    [Header("Level & XP")]
    public int playerLevel = 1;
    public int maxLevel = 10;
    [Tooltip("Slider showing XP progress")]
    public Slider xpSlider;
    [Tooltip("Level Up Screen GameObject (to be enabled when leveling up)")]
    public GameObject levelUpScreen;

    void Start()
    {
        timer = gameDuration;
        if (levelUpScreen != null)
            levelUpScreen.SetActive(false);
    }

    void Update()
    {
        // Count down the game timer.
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            EndGame();
        }
        UpdateTimerUI();

        // (Optional) Here you could add logic to fill the XP bar over time
        // and call OnPlayerLevelUp() when enough XP is earned.
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over! Time's up.");
        // Add game-over logic here.
    }

    /// <summary>
    /// Call this method when the player levels up.
    /// </summary>
    public void OnPlayerLevelUp()
    {
        playerLevel++;
        if (playerLevel > maxLevel)
            playerLevel = maxLevel;
        ShowLevelUpScreen();
    }

    void ShowLevelUpScreen()
    {
        if (levelUpScreen != null)
        {
            levelUpScreen.SetActive(true);
            // Populate the screen with upgrade options for weapons and passives.
        }
    }
}
