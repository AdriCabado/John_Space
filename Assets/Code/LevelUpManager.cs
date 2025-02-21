using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public int playerLevel = 1; // Start at level 1
    public PlayerWeaponController weaponController; // Assign in Inspector
    public XPManager xpManager; // Assign in Inspector

    public void LevelUp()
    {
        playerLevel++;
        Debug.Log("Player leveled up! Current Level: " + playerLevel);

        // Ensure weaponController exists before calling UnlockWeapon
        if (weaponController != null)
        {
            weaponController.UnlockWeapon(playerLevel);
        }
        else
        {
            Debug.LogError("PlayerWeaponController not assigned in LevelUpManager!");
        }

        // Reset XP after leveling up
        if (xpManager != null)
        {
            xpManager.ResetXP();
        }
    }
}
