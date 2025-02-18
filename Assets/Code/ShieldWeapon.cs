using UnityEngine;

public class ShieldWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Shield;
        cooldown = 0f; // Always active
        damage = 10f;
    }

    public override void Fire()
    {
        // For a shield, “firing” might mean keeping it active.
        Debug.Log("Shield active around John!");
        // The shield might be a constantly enabled collider that damages enemies on contact.
    }

    protected override void UpgradeWeapon()
    {
        if (passiveType == PassiveType.Titanium)
        {
            Debug.Log("Shield upgraded with Titanium! Armor increased and reflective damage boosted.");
            damage *= 1.5f;
            // Optionally, find the PlayerStats component and improve armor.
            PlayerStats stats = GetComponentInParent<PlayerStats>();
            if (stats != null)
            {
                stats.armorModifier = 0.7f; // John takes less damage.
            }
        }
    }
}
