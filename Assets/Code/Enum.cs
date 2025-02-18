// Enums.cs
public enum WeaponType
{
    Laser = 1,
    Cannon = 2,
    RocketLauncher = 3,
    ElectricField = 4,
    Shield = 5
}

public enum PassiveType
{
    Broadbeam = 1,   // Increases weapon size/range.
    Quickshot = 2,   // Shorter weapon cooldown.
    Overcharge = 3,  // Increases damage.
    Stunwave = 4,    // Adds stun effect.
    Titanium = 5     // Grants extra armor.
}
