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
    None = 0,      // Indicates no passive is applied.
    Broadbeam = 1, // Increases weapon size/range.
    Quickshot = 2, // Shortens weapon cooldown.
    Overcharge = 3,// Increases damage.
    Stunwave = 4,  // Adds a stun effect.
    Titanium = 5   // Grants extra armor.
}

