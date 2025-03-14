using UnityEngine;

public class RocketWeapon : Weapon
{
    [Header("Rocket Launcher Settings")]
    [Tooltip("Rocket prefab to instantiate when firing.")]
    public GameObject rocketPrefab;

    [Tooltip("Fire point transform for the rocket.")]
    public Transform firePoint;
    
    [Tooltip("Fixed speed at which the rocket travels.")]
    public float rocketSpeed = 10f;

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
        
        // Always fire in a random direction.
        float randomAngle = Random.Range(0f, 360f);
        Vector3 targetDirection = Quaternion.Euler(0, 0, randomAngle) * Vector3.right;
        Debug.Log("Firing rocket in random direction, angle: " + randomAngle);

        // Calculate rotation so that the rocket's up vector aligns with targetDirection.
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);


        // Instantiate the rocket.
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, targetRotation);

        // Set the rocket's velocity directly.
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = targetDirection * rocketSpeed;
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
