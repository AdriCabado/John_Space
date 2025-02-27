using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Timer")]
    [Tooltip("Game duration in seconds (300 = 5 minutes or 600 = 10 minutes)")]
    public float gameDuration = 300f;
    private float timer;
    private bool hasWon = false;

    [Tooltip("UI Text to display the timer (mm:ss)")]
    public TextMeshProUGUI timerText;

    [Header("Level & XP")]
    public int playerLevel = 1;
    public int maxLevel = 10;
    [Tooltip("Slider showing XP progress")]
    public Slider xpSlider;
    [Tooltip("Level Up Screen GameObject (to be enabled when leveling up)")]
    public GameObject levelUpScreen;

    [Header("Win Settings")]
    [Tooltip("John's GameObject (must have an Animator with a 'Teleport' trigger)")]
    public GameObject john;
    private Animator johnAnimator;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    void Start()
    {
        timer = gameDuration;
        if (levelUpScreen != null)
            levelUpScreen.SetActive(false);

        if (john != null)
        {
            johnAnimator = john.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (!hasWon)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
                EndGame();
                hasWon = true;
            }
            UpdateTimerUI();
        }

        // (Optional) Add any XP bar logic here.
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
        Debug.Log("You win! Time's up.");
        // Trigger John's teleport animation.
        if (johnAnimator != null)
        {
            johnAnimator.SetTrigger("Teleport");
        }
        // Add any additional win logic here (e.g., display a win screen, disable player controls, etc.)
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
