using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    [HideInInspector] public int currentHP;

    [Header("XP Settings")]
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int playerLevel = 1;
    public int maxLevel = 10;

    [Header("UI References")]
    public Image johnImage;
    public Image hpBarImage;
    public TextMeshProUGUI hpText;
    public Image xpBarImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelUpMessage;

    [Header("Game Over UI")]
    [Tooltip("Game Over screen UI object (hidden by default)")]
    public GameObject gameOverScreen;
    [Tooltip("Game Over text component")]
    public TextMeshProUGUI gameOverText;
    [Tooltip("Restart button")]
    public Button restartButton;


    [Header("Armor Modifier")]
    public float armorModifier = 1f;

    [Header("Weapon Unlock System")]
    public GameObject[] weaponIcons;

    public GameObject[] weaponPrefabs;

    private string[] weaponNames = { "Cannon", "Laser", "Rocket Launcher", "Electric Field", "Shield" };
    private string[] passiveNames = { "Shoot speed up", "Max duration", "Bigger explosions", "Larger field distance", "Titanium armor" };

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthUI();
        UpdateXPUI();
    }

    public void TakeDamage(int damage)
    {
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

        // Freeze the game
        Time.timeScale = 0f;

        // Activate the Game Over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
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
        if (playerLevel >= maxLevel) return;

        playerLevel++;
        currentXP -= xpToNextLevel;
        xpToNextLevel = Mathf.FloorToInt(xpToNextLevel * 1.2f);

        if (levelText != null)
        {
            levelText.text = "Level " + playerLevel;
        }

        UnlockUpgrade(playerLevel);
        UpdateXPUI();
    }

    void UnlockUpgrade(int level)
    {
        if (level >= 1 && level <= 5)
        {
            string weaponName = weaponNames[level - 1];
            ShowLevelUpMessage(weaponName + " Unlocked!");
            weaponIcons[level - 1].SetActive(true);
            weaponPrefabs[level - 1].SetActive(true);
        }
        else if (level >= 6 && level <= 10)
        {
            string passiveName = passiveNames[level - 6];
            ShowLevelUpMessage(passiveName + " Acquired!");
            weaponIcons[level - 6].GetComponent<Image>().color = Color.yellow;
        }
    }

    void ShowLevelUpMessage(string message)
    {
        if (levelUpMessage != null)
        {
            levelUpMessage.text = "Level Up! " + message;
            levelUpMessage.gameObject.SetActive(true);
            Invoke("HideLevelUpMessage", 2f);
        }
    }

    void HideLevelUpMessage()
    {
        if (levelUpMessage != null)
        {
            levelUpMessage.gameObject.SetActive(false);
        }
    }

    void UpdateXPUI()
    {
        if (xpBarImage != null)
        {
            xpBarImage.fillAmount = (float)currentXP / xpToNextLevel;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(10);
            StartCoroutine(FlashPortrait());
        }
    }

    private IEnumerator FlashPortrait()
    {
        johnImage.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        johnImage.color = Color.white;
    }
}
