using UnityEngine;

public class ShieldWeapon : Weapon
{
    [Header("Shield Launcher Settings")]
    [Tooltip("Reference to the shield prefab that will be toggled on/off.")]
    public GameObject shieldPrefab;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Shield;
        damage = 9999f; // Arbitrary high damage so that if used, it destroys asteroids instantly.
        
        // Activate the shield prefab (its own script will manage disappearance and reappearance)
        if (shieldPrefab != null)
        {
            shieldPrefab.SetActive(true);
        }
    }

    protected override void Update()
    {
        base.Update();
        // No additional cyclic behavior here; see ShieldPrefab.cs for collision handling.
    }

    public override void Fire()
    {
        // Not used because the shield's behavior is autonomous.
    }

    protected override void UpgradeWeapon()
    {
        // Optionally upgrade properties here.
    }
}
