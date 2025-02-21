using UnityEngine;

public class XPManager : MonoBehaviour
{
    public int currentXP = 0;
    public int xpToNextLevel = 100; // XP needed to level up
    public LevelUpManager levelUpManager;

    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            levelUpManager.LevelUp();
        }
    }

    public void ResetXP()
    {
        currentXP = 0;
    }
}
