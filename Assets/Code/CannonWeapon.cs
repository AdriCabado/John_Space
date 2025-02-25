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
        cooldown = 0.75f;  // Set your desired cooldown (seconds)
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
                
                GameObject bullet1 = Instantiate(bulletPrefab, spawnPos + new Vector3(-0.27f, 0f, 0f), spawnRot);
                GameObject bullet2 = Instantiate(bulletPrefab, spawnPos + new Vector3(0.27f, 0f, 0f), spawnRot);

                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                if (rb1 != null)
                {
                    rb1.AddForce(spawnRot * Vector2.right * 500f);
                }
                else
                {
                    Debug.LogWarning("Rigidbody2D component not found on bulletPrefab (bullet1).");
                }

                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                if (rb2 != null)
                {
                    rb2.AddForce(spawnRot * Vector2.right * 500f);
                }
                else
                {
                    Debug.LogWarning("Rigidbody2D component not found on bulletPrefab (bullet2).");
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