using UnityEngine;

public class ElectricFieldWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.ElectricField;
        cooldown = 0f; // Once activated, it stays active
        damage = 15f;
    }

    public override void Fire()
    {
        // Activate or maintain the electric field around John.
        Debug.Log("Electric Field active!");
        // Here you might enable an area-of-effect collider that deals damage.
    }

    protected override void UpgradeWeapon()
    {
        if (passiveType == PassiveType.Stunwave)
        {
            Debug.Log("Electric Field upgraded with Stunwave! Enemies are stunned on hit.");
            // Add stun functionality to your collision/damage logic.
        }
    }
}
