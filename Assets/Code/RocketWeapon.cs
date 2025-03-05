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
        cooldown = 2f;  // Fires every 2 seconds
        damage = 30f;   // Base damage for the rocket
    }

    protected override void Update()
    {
        base.Update();
        // Automatically fire when the cooldown expires.
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

        // Default firing direction: use firePoint's up vector (assuming rocket's forward is up).
        Vector3 targetDirection = firePoint.up;

        // Look for nearby asteroids using the "Asteroid" tag.
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
                Debug.Log("Firing rocket toward asteroid at distance: " + minDist);
            }
        }
        else
        {
            // No asteroids found, fire in a random direction.
            float randomAngle = Random.Range(0f, 360f);
            targetDirection = Quaternion.Euler(0, 0, randomAngle) * Vector3.up;
            Debug.Log("No asteroids found. Firing rocket in random direction, angle: " + randomAngle);
        }

        // Calculate rotation so that the rocket's local up (forward) matches targetDirection.
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Instantiate the rocket with the calculated rotation.
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, targetRotation);
        if (rocket == null)
        {
            Debug.LogWarning("Rocket instantiation failed.");
            return;
        }

        // Apply an impulse force along the rocket's up direction at reduced speed.
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 force = rocket.transform.up * 10f;
            rb.AddForce(force, ForceMode2D.Impulse);
            Debug.Log("Applied force: " + force);
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
            Debug.Log("Rocket Launcher upgraded with Overcharge! Reduced cooldown.");
            cooldown = 1f;
        }
    }
}
