using UnityEngine;

public class CannonWeapon : Weapon
{
    [Header("Bullet Settings")]
    [Tooltip("Bullet prefab to instantiate when firing.")]
    public GameObject bulletPrefab;

    [Tooltip("Fire point transform where the bullet will spawn.")]
    public Transform firePoint;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Cannon;
        cooldown = 1f;  // Set your desired cooldown (seconds)
        damage = 20f;   // Base damage of the cannon
    }

    public override void Fire()
    {
        if (cooldownTimer <= 0f)
        {
            if (bulletPrefab != null)
            {
                // Use the firePoint if it's set; otherwise use the cannon's own transform.
                Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;
                Quaternion spawnRot = (firePoint != null) ? firePoint.rotation : transform.rotation;
                
                // Instantiate the bullet at the specified position and rotation.
                GameObject bullet = Instantiate(bulletPrefab, spawnPos, spawnRot);

                // Optionally, add force to the bullet's Rigidbody2D.
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Apply force in the direction the firePoint is facing.
                    rb.AddForce(spawnRot * Vector2.right * 500f); // Adjust the force value as needed.
                }
                else
                {
                    Debug.LogWarning("Rigidbody2D component not found on bulletPrefab.");
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