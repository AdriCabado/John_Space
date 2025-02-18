using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Current Equipment")]
    [Tooltip("Reference to the current weapon component attached to John")]
    public Weapon currentWeapon;
    
    [Tooltip("Store the current passive (if any) applied to the weapon")]
    public PassiveType? currentPassive = null;

    void Update()
    {
        // For testing: press Space to fire the current weapon.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentWeapon != null)
            {
                currentWeapon.Fire();
            }
        }
    }

    /// <summary>
    /// Fuse the current weapon with a passive (if they match by number).
    /// </summary>
    public void FuseWeaponWithPassive(PassiveType passive)
    {
        if (currentWeapon != null)
        {
            currentWeapon.FusePassive(passive);
            currentPassive = passive;
        }
    }
}
