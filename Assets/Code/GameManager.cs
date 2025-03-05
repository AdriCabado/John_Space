using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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
    [Tooltip("Win screen UI object (hidden by default)")]
    public GameObject winScreen;
    [Tooltip("Win text component")]
    public TextMeshProUGUI winText;
    [Tooltip("Restart button")]
    public Button restartButton;

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

            // Stop John's movement
            Rigidbody johnRigidbody = john.GetComponent<Rigidbody>();
            if (johnRigidbody != null)
            {
            johnRigidbody.velocity = Vector3.zero;
            johnRigidbody.angularVelocity = Vector3.zero;
            }
           
            foreach (Transform child in john.transform)
            {
                if (child.name != "CameraFollowPoint")
                {
                    child.gameObject.SetActive(false);
                }
            }
            
        }
        // Deactivate John object after the teleport animation
        if (john != null)
        {
            StartCoroutine(DeactivateJohnAfterDelay(1f)); // Adjust the delay as needed to match the animation duration
        }
        // Start coroutine to wait before freezing the game
        StartCoroutine(WaitAndFreezeGame(2f));
    }

    IEnumerator DeactivateJohnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        john.SetActive(false);
    }

    IEnumerator WaitAndFreezeGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // Add any additional win logic here (e.g., display a win screen, disable player controls, etc.)
        // Freeze the game
        Time.timeScale = 0f;
        // Activate the Win screen
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }

        // Ensure the restart button works
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners(); // Clear previous listeners
            restartButton.onClick.AddListener(RestartGame);
        }
    }
    void RestartGame()
    {
        // Unfreeze time
        Time.timeScale = 1f;
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
