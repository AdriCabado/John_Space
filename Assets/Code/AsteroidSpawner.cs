using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [Tooltip("Asteroid prefab to spawn.")]
    public GameObject asteroidPrefab;

    [Tooltip("Time interval between spawns.")]
    public float spawnInterval = 2f;

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
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnAsteroid();
            timer = spawnInterval;
        }
    }

    void SpawnAsteroid()
    {
        // Get camera boundaries in world space
        Vector3 camPos = mainCamera.transform.position;
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        // Randomly choose one of four sides: 0 = top, 1 = bottom, 2 = left, 3 = right.
        int side = Random.Range(0, 4);
        Vector3 spawnPos = Vector3.zero;

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

        // Instantiate the asteroid and set its movement target
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        AsteroidMovement movement = asteroid.GetComponent<AsteroidMovement>();
        if (movement != null && johnTransform != null)
        {
            movement.SetTarget(johnTransform.position);
        }
    }
}
