using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [Tooltip("Asteroid prefab to spawn.")]
    public GameObject asteroidPrefab;

    [Tooltip("Base time interval between spawns (in seconds) at the start.")]
    public float baseSpawnInterval = 1f;  // Lowered from 2f for more intensity early on

    [Tooltip("Amount to reduce the spawn interval for each minute passed.")]
    public float reductionPerMinute = 0.3f; // Adjusted for a smoother curve

    [Tooltip("Minimum spawn interval (in seconds) allowed.")]
    public float minSpawnInterval = 0.1f;

    [Tooltip("Distance outside the camera bounds to spawn the asteroids.")]
    public float spawnDistance = 2f;

    [Header("Target Settings")]
    [Tooltip("Reference to John's Transform so asteroids move toward him.")]
    public Transform johnTransform;

    private Camera mainCamera;
    private float spawnTimer;
    private float elapsedSpawnTime = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        spawnTimer = baseSpawnInterval;
        elapsedSpawnTime = 0f;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        elapsedSpawnTime += dt;
        spawnTimer -= dt;
        if (spawnTimer <= 0f)
        {
            SpawnAsteroid();

            // Calculate elapsed minutes since spawner started.
            float elapsedMinutes = elapsedSpawnTime / 60f;
            // Calculate the current spawn interval.
            float currentSpawnInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval - reductionPerMinute * elapsedMinutes);
            spawnTimer = currentSpawnInterval;
        }
    }

    void SpawnAsteroid()
    {
        // Get camera boundaries in world space.
        Vector3 camPos = mainCamera.transform.position;
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        Vector3 spawnPos = Vector3.zero;
        
        // Randomly choose one of four sides: 0 = top, 1 = bottom, 2 = left, 3 = right.
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // top
                spawnPos = new Vector3(
                    Random.Range(camPos.x - camWidth / 2, camPos.x + camWidth / 2),
                    camPos.y + camHeight / 2 + spawnDistance,
                    0f);
                break;
            case 1: // bottom
                spawnPos = new Vector3(
                    Random.Range(camPos.x - camWidth / 2, camPos.x + camWidth / 2),
                    camPos.y - camHeight / 2 - spawnDistance,
                    0f);
                break;
            case 2: // left
                spawnPos = new Vector3(
                    camPos.x - camWidth / 2 - spawnDistance,
                    Random.Range(camPos.y - camHeight / 2, camPos.y + camHeight / 2),
                    0f);
                break;
            case 3: // right
                spawnPos = new Vector3(
                    camPos.x + camWidth / 2 + spawnDistance,
                    Random.Range(camPos.y - camHeight / 2, camPos.y + camHeight / 2),
                    0f);
                break;
        }

        // Instantiate the asteroid.
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        AsteroidMovement movement = asteroid.GetComponent<AsteroidMovement>();
        if (movement != null && johnTransform != null)
        {
            movement.SetTarget(johnTransform.position);
        }
    }
}
