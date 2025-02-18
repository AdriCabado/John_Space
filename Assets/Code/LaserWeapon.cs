using UnityEngine;

public class LaserWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Laser;
        cooldown = 0f; // continuous, so no cooldown is needed
        damage = 10f;
    }

    public override void Fire()
    {
        // For a continuous beam, you might have it on whenever the fire button is held.
        Debug.Log("Laser active (continuous beam)");
        // Implement beam logic (e.g. enable a LineRenderer, detect collisions, etc.)
    }

    protected override void UpgradeWeapon()
    {
        if (passiveType == PassiveType.Broadbeam)
        {
            Debug.Log("Laser upgraded with Broadbeam! Beam widened and damage increased.");
            // Example: increase beam width and damage.
            damage *= 1.5f;
            // (If you have a collider or visual beam, increase its size here.)
        }
    }
}
