using UnityEngine;

public class ElectricFieldWeapon : Weapon
{
    [Header("Electric Field Settings")]
    [Tooltip("Prefab for the electric field effect.")]
    public GameObject electricFieldPrefab;
    
    [Tooltip("Distance from John's center at which the field orbits.")]
    public float orbitDistance = 2f;
    
    [Tooltip("Rotation speed (in degrees per second) of the electric field around John.")]
    public float rotationSpeed = 90f;
    
    // The instance of the electric field effect.
    private GameObject electricFieldInstance;
    // Current angle in degrees.
    private float currentAngle = 0f;
    
    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.ElectricField;
        // For continuous effects, we don't necessarily need a cooldown.
        cooldown = 0f;
        // Damage is set high so that the field destroys asteroids on contact.
        damage = 9999f;
        
        // Instantiate the electric field effect as a child of John.
        if (electricFieldPrefab != null)
        {
            // Start with an initial offset; for example, to the right of John.
            Vector3 initialOffset = new Vector3(orbitDistance, 0f, 0f);
            electricFieldInstance = Instantiate(electricFieldPrefab, transform.position + initialOffset, Quaternion.identity, transform);
        }
    }
    
    void Update()
    {
        // Rotate the electric field around John's center.
        if (electricFieldInstance != null)
        {
            // Increase the angle over time.
            currentAngle += rotationSpeed * Time.deltaTime;
            // Convert the current angle from degrees to radians.
            float rad = currentAngle * Mathf.Deg2Rad;
            // Calculate the new offset.
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * orbitDistance;
            // Update the electric field's position.
            electricFieldInstance.transform.position = transform.position + offset;
            // Optionally, make the field's "up" face outward.
            electricFieldInstance.transform.up = offset.normalized;
        }
    }
    
    public override void Fire()
    {
        // Not used – the electric field is continuous.
    }
    
    protected override void UpgradeWeapon()
    {
        // Example: if a passive is fused (e.g., Stunwave), you could boost the orbit distance or rotation speed.
        if (passiveType == PassiveType.Stunwave)
        {
            rotationSpeed *= 1.2f;
            orbitDistance *= 1.1f;
        }
    }
}
