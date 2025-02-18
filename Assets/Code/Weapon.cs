using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Base Settings")]
    public WeaponType weaponType;
    [Tooltip("Optional passive fused to this weapon")]
    public PassiveType? passiveType = null;
    public float cooldown;   // Seconds between shots (if applicable)
    public float damage;

    protected float cooldownTimer;

    protected virtual void Start()
    {
        cooldownTimer = 0f;
    }

    protected virtual void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Attempt to fire the weapon.
    /// </summary>
    public virtual void Fire()
    {
        if (cooldownTimer <= 0f)
        {
            Debug.Log("Firing " + weaponType.ToString());
            // Implement firing (e.g. instantiate bullets or beams)
            cooldownTimer = cooldown;
        }
    }

    /// <summary>
    /// Fuse this weapon with a passive if the numbers match.
    /// </summary>
    public void FusePassive(PassiveType passive)
    {
        // Only allow fusion if the passive number matches the weapon number.
        if ((int)weaponType == (int)passive)
        {
            passiveType = passive;
            UpgradeWeapon();
        }
        else
        {
            Debug.Log("Passive " + passive.ToString() + " cannot fuse with " + weaponType.ToString());
        }
    }

    /// <summary>
    /// Implement how the weapon improves when fused.
    /// </summary>
    protected abstract void UpgradeWeapon();
}

