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
    [Tooltip("Exit button on the Win screen")]
    public Button exitButton;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
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

            // Stop John's movement.
            Rigidbody johnRigidbody = john.GetComponent<Rigidbody>();
            if (johnRigidbody != null)
            {
                johnRigidbody.velocity = Vector3.zero;
                johnRigidbody.angularVelocity = Vector3.zero;
            }

            // Deactivate all children of John except the CameraFollowPoint.
            foreach (Transform child in john.transform)
            {
                if (child.name != "CameraFollowPoint")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        // Deactivate John after a short delay.
        if (john != null)
        {
            StartCoroutine(DeactivateJohnAfterDelay(1f));
        }
        // Wait a moment before freezing the game.
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
        // Freeze the game.
        Time.timeScale = 0f;
        // Activate the Win screen.
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }
        // Set up the restart button.
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }
        // Set up the exit button.
        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
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
