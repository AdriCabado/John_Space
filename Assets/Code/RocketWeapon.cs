using UnityEngine;

public class RocketWeapon : Weapon
{
    [Header("Rocket Launcher Settings")]
    [Tooltip("Rocket prefab to instantiate when firing.")]
    public GameObject rocketPrefab;

    [Tooltip("Fire point transform for the rocket.")]
    public Transform firePoint;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.RocketLauncher;
        cooldown = 2f;  // Rocket launcher fires every 2 seconds
        damage = 30f;   // Base damage (can be used by the rocket if desired)
    }

    public override void Fire()
    {
        if (cooldownTimer <= 0f)
        {
            if (rocketPrefab != null && firePoint != null)
            {
                // Default direction is the firePoint's up vector.
                Vector3 targetDirection = firePoint.up;

                // Find all asteroids in the scene.
                AsteroidHealth[] asteroids = FindObjectsOfType<AsteroidHealth>();
                if (asteroids.Length > 0)
                {
                    float minDist = Mathf.Infinity;
                    Transform nearestAsteroid = null;
                    foreach (AsteroidHealth asteroid in asteroids)
                    {
                        float dist = Vector3.Distance(firePoint.position, asteroid.transform.position);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            nearestAsteroid = asteroid.transform;
                        }
                    }
                    if (nearestAsteroid != null)
                    {
                        // Calculate direction from firePoint to the nearest asteroid.
                        targetDirection = (nearestAsteroid.position - firePoint.position).normalized;
                    }
                }

                // Calculate the rotation so that the rocket's up points toward targetDirection.
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

                // Instantiate the rocket with the calculated rotation.
                GameObject rocket = Instantiate(rocketPrefab, firePoint.position, targetRotation);
                Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Apply an impulse force along the rocket's up direction.
                    rb.AddForce(rocket.transform.up * 500f, ForceMode2D.Impulse);
                }
                else
                {
                    Debug.LogWarning("Rigidbody2D component not found on rocket prefab.");
                }
            }
            else
            {
                Debug.LogWarning("Rocket prefab or fire point not assigned in RocketWeapon.");
            }

            cooldownTimer = cooldown;
        }
    }

    protected override void UpgradeWeapon()
    {
        if (passiveType == PassiveType.Overcharge)
        {
            Debug.Log("Rocket Launcher upgraded with Overcharge! Increased damage.");
            damage *= 1.5f;
        }
    }
}
