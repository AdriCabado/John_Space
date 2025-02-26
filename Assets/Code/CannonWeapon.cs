using UnityEngine;

public class CannonWeapon : Weapon
{
    [Header("Bullet Settings")]
    [Tooltip("Bullet prefab to instantiate when firing.")]
    public GameObject bulletPrefab;

    [Tooltip("Fire point transform for the left bullet.")]
    public Transform firePointLeft;

    [Tooltip("Fire point transform for the right bullet.")]
    public Transform firePointRight;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Cannon;
        cooldown = 0.75f;  // Set your desired cooldown (seconds)
        damage = 20f;      // Base damage of the cannon
    }

    public override void Fire()
    {
        if (cooldownTimer <= 0f)
        {
            if (bulletPrefab != null)
            {
                if (firePointLeft != null && firePointRight != null)
                {
                    // Instantiate bullet at left fire point
                    GameObject bulletLeft = Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
                    // Instantiate bullet at right fire point
                    GameObject bulletRight = Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);

                    // Apply force to the left bullet using the fire point's right vector.
                    Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
                    if (rbLeft != null)
                    {
                        rbLeft.AddForce(firePointLeft.right * 500f);
                    }
                    else
                    {
                        Debug.LogWarning("Rigidbody2D component not found on bulletPrefab (left bullet).");
                    }

                    // Apply force to the right bullet using the fire point's right vector.
                    Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
                    if (rbRight != null)
                    {
                        rbRight.AddForce(firePointRight.right * 500f);
                    }
                    else
                    {
                        Debug.LogWarning("Rigidbody2D component not found on bulletPrefab (right bullet).");
                    }
                }
                else
                {
                    Debug.LogWarning("Fire point Left and/or Right are not assigned in the CannonWeapon component!");
                }
            }
            else
            {
                Debug.LogWarning("Bullet prefab is not assigned in the CannonWeapon component!");
            }

            cooldownTimer = cooldown;
        }
    }

    protected override void UpgradeWeapon()
    {
        if (passiveType == PassiveType.Quickshot)
        {
            Debug.Log("Cannon upgraded with Quickshot! Reduced cooldown.");
            cooldown *= 0.7f; // Reduces the cooldown by 30%
        }
    }
}
