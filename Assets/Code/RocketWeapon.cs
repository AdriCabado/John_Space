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
        cooldown = 2f;  // Rocket launcher fires every 2 seconds.
        damage = 30f;   // Base damage for the rocket.
    }

    protected override void Update()
    {
        base.Update();
        // Automatically fire when cooldown has expired.
        if (cooldownTimer <= 0f)
        {
            Fire();
        }
    }

    public override void Fire()
    {
        if (rocketPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Rocket prefab or fire point not assigned in RocketWeapon.");
            return;
        }

        // Default firing direction: use firePoint's right (assuming the rocket sprite points right).
        Vector3 targetDirection = firePoint.right;

        // Look for nearby asteroids.
        GameObject[] asteroidObjects = GameObject.FindGameObjectsWithTag("Asteroid");
        if (asteroidObjects.Length > 0)
        {
            float minDist = Mathf.Infinity;
            Transform nearestAsteroid = null;
            foreach (GameObject asteroid in asteroidObjects)
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
                targetDirection = (nearestAsteroid.position - firePoint.position).normalized;
            }
        }
        else
        {
            // No asteroids found, fire in a random direction.
            float randomAngle = Random.Range(0f, 360f);
            targetDirection = Quaternion.Euler(0, 0, randomAngle) * Vector3.right;
        }

        // Calculate rotation so that the rocket's forward (up) faces the target direction.
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Instantiate the rocket.
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, targetRotation);
        if (rocket == null)
        {
            Debug.LogWarning("Rocket instantiation failed.");
            return;
        }

        // Apply an impulse force along the rocket's right direction.
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(rocket.transform.up * 500f/* , ForceMode2D.Impulse */);
             /* rbRight.AddForce(firePointRight.right * 500f); */
        }
        else
        {
            Debug.LogWarning("Rigidbody2D not found on rocket prefab.");
        }

        cooldownTimer = cooldown;
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
