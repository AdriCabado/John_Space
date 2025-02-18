using UnityEngine;

public class RocketLauncherWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.RocketLauncher;
        cooldown = 2f;
        damage = 30f;
    }

    public override void Fire()
    {
        if (cooldownTimer <= 0f)
        {
            Debug.Log("Firing Rocket Launcher!");
            // Instantiate a rocket prefab that travels in a random direction
            // and on impact creates an AOE explosion.
            cooldownTimer = cooldown;
        }
    }

    protected override void UpgradeWeapon()
    {
        if (passiveType == PassiveType.Overcharge)
        {
            Debug.Log("Rocket Launcher upgraded with Overcharge! Damage increased.");
            damage *= 1.5f;
        }
    }
}
