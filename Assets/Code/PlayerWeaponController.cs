using UnityEngine;
using System.Collections.Generic;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Weapons System")]
    public List<Weapon> availableWeapons = new List<Weapon>(); // List of all weapons John can use
    private Weapon activeWeapon; // Currently active weapon

    private int nextWeaponUnlockLevel = 2; // Level at which new weapons start unlocking

    private void Start()
    {
        // Ensure the Cannon (WeaponType.Cannon) is active by default
        foreach (Weapon weapon in availableWeapons)
        {
            if (weapon.weaponType == WeaponType.Cannon)
            {
                activeWeapon = weapon;
                activeWeapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false); // Disable other weapons at start
            }
        }
    }

    private void Update()
    {
        if (activeWeapon != null)
        {
            activeWeapon.Fire(); // Auto-fire every frame
        }
    }

    /// <summary>
    /// Unlocks the next available weapon if John reaches the required level.
    /// </summary>
    public void UnlockWeapon(int playerLevel)
    {
        if (playerLevel >= nextWeaponUnlockLevel)
        {
            foreach (Weapon weapon in availableWeapons)
            {
                if (!weapon.gameObject.activeSelf) // Find the first locked weapon
                {
                    weapon.gameObject.SetActive(true);
                    Debug.Log("Unlocked new weapon: " + weapon.weaponType);
                    nextWeaponUnlockLevel += 2; // Example: Unlock weapons every 2 levels
                    break;
                }
            }
        }
    }
}
