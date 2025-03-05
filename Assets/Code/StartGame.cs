using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Panel for the start screen")]
    public GameObject startScreen;
    [Tooltip("Instruction text: 'Use WASD or the Arrow Keys to move and start the game'")]
    public TextMeshProUGUI instructionText;
    [Tooltip("Exit button on the start screen")]
    public Button exitButton;

    void Start()
    {
        // Freeze game at start
        Time.timeScale = 0f;

        if (startScreen != null)
        {
            startScreen.SetActive(true);
        }

        // Set up the exit button to quit the game.
        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    void Update()
    {
        // Check if any of the movement keys are pressed
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartGamePlay();
        }
    }

    void StartGamePlay()
    {
        // Unfreeze the game and hide the start screen
        Time.timeScale = 1f;
        if (startScreen != null)
        {
            startScreen.SetActive(false);
        }
    }

    void ExitGame()
    {
        Debug.Log("Exiting game from start screen...");
        Application.Quit();
    }
}
