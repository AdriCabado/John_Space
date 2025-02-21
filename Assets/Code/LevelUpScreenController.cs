using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreenController : MonoBehaviour
{
    [Header("Upgrade Slot Buttons")]
    public Button weaponUpgradeButton;
    public Button passiveUpgradeButton;

    void Start()
    {
        // Hook up button callbacks (or set them in the Inspector)
        if (weaponUpgradeButton != null)
            weaponUpgradeButton.onClick.AddListener(OnWeaponUpgradeSelected);
        if (passiveUpgradeButton != null)
            passiveUpgradeButton.onClick.AddListener(OnPassiveUpgradeSelected);
    }

    /// <summary>
    /// Call this method to open the level-up screen.
    /// </summary>
    public void ShowLevelUpScreen()
    {
        gameObject.SetActive(true);
        // Optionally, update the UI with available upgrade choices.
    }

    void OnWeaponUpgradeSelected()
    {
        Debug.Log("Weapon upgrade selected.");
        // Tell the PlayerWeaponController to fuse the current weapon with its matching passive.
        // For example, if current weapon is Laser, fuse with Broadbeam.
        // (You might have a lookup table or make the choice from UI.)
        PlayerWeaponController pwc = FindObjectOfType<PlayerWeaponController>();
        if (pwc != null)
        {
            // Example: if weapon type Laser (1), fuse with Broadbeam.
           
        }
        gameObject.SetActive(false);
    }

    void OnPassiveUpgradeSelected()
    {
        Debug.Log("Passive upgrade selected.");
        // Here you could offer a passive upgrade choice (or auto-upgrade based on your design).
        // For demonstration, assume you upgrade the passive for the current weapon.
        PlayerWeaponController pwc = FindObjectOfType<PlayerWeaponController>();
        if (pwc != null)
        {
            // Example: if weapon type Cannon (2), fuse with Quickshot.
        }
        gameObject.SetActive(false);
    }
}
