using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float speed = 5f;
    public float spawnRangeX = 10f;
    public float spawnRangeY = 5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAsteroid();
        CheckBounds();
    }

    void SpawnAsteroid()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomY = Random.Range(-spawnRangeY, spawnRangeY);
        transform.position = new Vector3(randomX, randomY, 0);
    }

    void MoveAsteroid()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void CheckBounds()
    {
        if (transform.position.y < -spawnRangeY)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("John") || collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
