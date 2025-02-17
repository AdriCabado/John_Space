using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed;
    private GameObject nearestAsteroid;
    private List<GameObject> asteroids;

    void Start()
    {
        asteroids = new List<GameObject>(); // Initialize the asteroids list
    }

    void Update()
    {
        float minDistance = Mathf.Infinity;
        nearestAsteroid = null; // Ensure nearestAsteroid is reset each frame

        foreach (GameObject asteroid in asteroids)
        {
            float distance = Vector3.Distance(transform.position, asteroid.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestAsteroid = asteroid;
            }
        }

        if (nearestAsteroid != null)
        {
            Vector3 direction = (nearestAsteroid.transform.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<BulletScript>().speed;
        }

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GetComponent<Rigidbody2D>().velocity = movement * speed;
    }
}
