using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [Tooltip("Asteroid prefab to spawn.")]
    public GameObject asteroidPrefab;

     [Tooltip("Base time interval between spawns (in seconds) at the start.")]
    public float baseSpawnInterval = 2f;

    [Tooltip("Amount to reduce the spawn interval for each minute passed.")]
    public float reductionPerMinute = 0.5f;

    [Tooltip("Minimum spawn interval (in seconds) allowed.")]
    public float minSpawnInterval = 0.5f;

    [Tooltip("Distance outside the camera bounds to spawn the asteroids.")]
    public float spawnDistance = 2f;

    [Header("Target Settings")]
    [Tooltip("Reference to John's Transform so asteroids move toward him.")]
    public Transform johnTransform;

    private Camera mainCamera;
    private float timer;

     void Start()
    {
        mainCamera = Camera.main;
        // Start with the base spawn interval.
        timer = baseSpawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnAsteroid();

            // Calculate how many minutes have elapsed.
            // Assuming the game run lasts 5 minutes (300 seconds).
            float elapsedTime = (300f - GameManager.instance.gameDuration + (300f - timer)); 
            // Alternatively, if you have a central timer, you can use that.
            // For this example, we'll compute elapsed time as (baseDuration - timerElapsed)
            // Let's assume you don't have an external timer, so we'll simulate:
            float elapsedMinutes = (300f - timer) / 60f;  // This is a rough estimate

            // For a more robust solution, consider referencing your GameManager's elapsed time.

            // Calculate current spawn interval: 
            // current interval = base interval - (minutesPassed * reductionPerMinute)
            float currentSpawnInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval - reductionPerMinute * elapsedMinutes);
            timer = currentSpawnInterval;
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

         // Instantiate the asteroid and set its movement target.
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        AsteroidMovement movement = asteroid.GetComponent<AsteroidMovement>();
        if (movement != null && johnTransform != null)
        {
            movement.SetTarget(johnTransform.position);
        }
    }
}
