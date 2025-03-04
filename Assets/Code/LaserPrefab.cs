using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPrefab : MonoBehaviour
{
        

    [Tooltip("Damage dealt by this laser.")]
    public float damage = 10f;

    void Start()
    {
    }

    // This example uses collision detection. If your bullet uses a trigger, replace with OnTriggerEnter2D.
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Asteroid"))
    {
        Debug.Log("Laser hit asteroid!");
        AsteroidHealth asteroid = other.GetComponent<AsteroidHealth>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(damage);
        }
    }
}
}
