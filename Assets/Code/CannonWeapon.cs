using UnityEngine;

public class CannonWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Cannon;
        cooldown = 1f;
        damage = 20f;
    }

    public override void Fire()
    {
        if (cooldownTimer <= 0f)
        {
            Debug.Log("Firing Cannon!");
            // Instantiate your bullet prefab here, e.g.:
            // Instantiate(bulletPrefab, transform.position, transform.rotation);
            cooldownTimer = cooldown;
        }
    }

    protected override void UpgradeWeapon()
    {
        if (passiveType == PassiveType.Quickshot)
        {
            Debug.Log("Cannon upgraded with Quickshot! Reduced cooldown.");
            cooldown *= 0.7f; // shorten the cooldown by 30%
        }
    }
}
