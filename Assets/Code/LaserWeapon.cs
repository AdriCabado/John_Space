using UnityEngine;

public class LaserWeapon : Weapon
{
    [Header("Laser Settings")]
    [Tooltip("The visual representation of the laser beam (child GameObject).")]
    public GameObject laserBeam;
    
    [Tooltip("Duration (in seconds) the laser remains active each cycle.")]
    public float activeDuration = 2f;
    
    [Tooltip("Cooldown duration (in seconds) after the laser deactivates.")]
    public float cooldownDuration = 1f;
    
    // Timer for switching between active and cooldown states.
    private float stateTimer;
    // Tracks whether the laser is currently active.
    private bool isActive = false;
    
    protected override void Start()
    {
        base.Start();
        // Set the weapon type to Laser.
        weaponType = WeaponType.Laser;
        
        // Immediately activate the laser at start.
        isActive = true;
        stateTimer = GetEffectiveActiveDuration();
        if (laserBeam != null)
        {
            laserBeam.SetActive(true);
        }
    }
    
    protected override void Update()
    {
        base.Update();
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
        {
            if (isActive)
            {
                // Transition from active to cooldown.
                isActive = false;
                stateTimer = cooldownDuration;
                if (laserBeam != null)
                {
                    laserBeam.SetActive(false);
                }
            }
            else
            {
                // Transition from cooldown to active.
                isActive = true;
                stateTimer = GetEffectiveActiveDuration();
                if (laserBeam != null)
                {
                    laserBeam.SetActive(true);
                }
            }
        }
    }
    
    // Since this weapon auto-fires via the Update loop, Fire() remains unused.
    public override void Fire()
    {
        // Intentionally left blank.
    }
    
    // Calculate effective active duration, increasing it by 50% if a passive is fused.
    private float GetEffectiveActiveDuration()
    {
        float duration = activeDuration;
        if (passiveType != PassiveType.None)
        {
            duration *= 1.5f;
        }
        return duration;
    }
    
    // When the laser is upgraded via its passive, increase the base active duration by 50%.
    protected override void UpgradeWeapon()
    {
        activeDuration *= 1.5f;
    }
}
