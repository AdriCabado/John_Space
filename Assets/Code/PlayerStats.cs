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
     [Tooltip("Exit button on the Game Over screen")]
    public Button exitButton;

    [Header("Armor Modifier")]
    public float armorModifier = 1f;

    [Header("Weapon Unlock System")]
    public GameObject[] weaponIcons;
    public GameObject[] weaponPrefabs; // Order: Cannon, Laser, Rocket, Electric Field, Shield

    private string[] weaponNames = { "Cannon", "Laser", "Rocket Launcher", "Electric Field", "Shield" };
    private string[] passiveNames = { "Cannon shoot speed up", "Laser max duration up", "More rockets", "Faster electric field rotation", "Less shield recharge time" };

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
        Time.timeScale = 0f;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }
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
        xpToNextLevel = Mathf.FloorToInt(xpToNextLevel * 1.4f);

        if (levelText != null)
        {
            levelText.text = "Level " + playerLevel;
        }

        UnlockUpgrade(playerLevel);
        UpdateXPUI();
    }

    // Modified UnlockUpgrade: For levels 1-5 unlock base weapons,
    // For levels 6-10, automatically upgrade the corresponding weapon's passive.
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
            int upgradeIndex = level - 6; // Mapping: level 6 -> index 0, level 7 -> index 1, etc.
            string passiveName = passiveNames[upgradeIndex];
            ShowLevelUpMessage(passiveName + " Acquired!");
            weaponIcons[upgradeIndex].GetComponent<Image>().color = Color.yellow;

            switch (level)
            {
                case 6:
                    {
                        // Upgrade CannonWeapon: reduce cooldown to 0.5f.
                        CannonWeapon cannon = weaponPrefabs[0].GetComponent<CannonWeapon>();
                        if (cannon != null)
                        {
                            cannon.cooldown = 0.5f;
                            Debug.Log("CannonWeapon passive applied: cooldown reduced to 0.5f");
                        }
                        break;
                    }
                case 7:
                    {
                        // Upgrade LaserWeapon: set active duration to 3f.
                        LaserWeapon laser = weaponPrefabs[1].GetComponent<LaserWeapon>();
                        if (laser != null)
                        {
                            laser.activeDuration = 3f;
                            Debug.Log("LaserWeapon passive applied: active duration set to 3f");
                        }
                        break;
                    }
                case 8:
                    {
                        // Upgrade RocketWeapon: reduce cooldown to 1f.
                        RocketWeapon rocket = weaponPrefabs[2].GetComponent<RocketWeapon>();
                        if (rocket != null)
                        {
                            rocket.cooldown = 1f;
                            Debug.Log("RocketWeapon passive applied: cooldown reduced to 1f");
                        }
                        break;
                    }
                case 9:
                    {
                        // Upgrade ElectricFieldWeapon: set rotation speed to 120f.
                        ElectricFieldWeapon electricField = weaponPrefabs[3].GetComponent<ElectricFieldWeapon>();
                        if (electricField != null)
                        {
                            electricField.rotationSpeed = 120f;
                            Debug.Log("ElectricFieldWeapon passive applied: rotation speed set to 120f");
                        }
                        break;
                    }
                case 10:
                    {
                        // Upgrade Shield: reduce reappearDelay to 5f.
                        ShieldWeapon shieldWeapon = weaponPrefabs[4].GetComponent<ShieldWeapon>();
                        if (shieldWeapon != null)
                        {
                            ShieldPrefab shieldPrefabScript = shieldWeapon.GetComponentInChildren<ShieldPrefab>();
                            if (shieldPrefabScript != null)
                            {
                                shieldPrefabScript.reappearDelay = 5f;
                                Debug.Log("Shield passive applied: reappearDelay reduced to 5f");
                            }
                        }
                        break;
                    }
            }
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
        Debug.Log(message);
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
            TakeDamage(20);
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
