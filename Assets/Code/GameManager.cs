using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public float spawnRate = 2f;
    private float nextSpawnTime = 0f;

    public float gameDuration = 600f; // 10 minutes in seconds
    private float elapsedTime = 0f;

    public GameObject enemyPrefab; // Add this if not already present
    public float asteroidSpeed = 5f; // Add this if not already present

    private int playerHealth;
    private int playerScore;
    private bool isGameOver;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= gameDuration)
            {
                GameOver();
            }
            else
            {
                UpdateGame();
            }
    
            if (Time.time >= nextSpawnTime)
            {
                SpawnAsteroid();
                nextSpawnTime = Time.time + spawnRate;
            }
        }
    }
    
    void UpdateGame()
    {
        // Add your logic for updating the game here
    }

    void StartGame()
    {
        playerHealth = 100;
        playerScore = 0;
        isGameOver = false;
        elapsedTime = 0f;
        nextSpawnTime = Time.time + spawnRate;
    }

    void SpawnAsteroid()
{
    Vector3 spawnPosition = CalculateSpawnPosition();
    Debug.Log("Spawning asteroid at: " + spawnPosition);
    GameObject asteroid = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    asteroid.GetComponent<Rigidbody2D>().velocity = (Vector2.zero - (Vector2)asteroid.transform.position).normalized * asteroidSpeed;
}

Vector3 CalculateSpawnPosition()
{
    Camera mainCamera = Camera.main;
    float cameraHeight = mainCamera.orthographicSize * 2;
    float cameraWidth = cameraHeight * mainCamera.aspect;

    float spawnX = Random.Range(-cameraWidth / 2, cameraWidth / 2);
    float spawnY = Random.Range(-cameraHeight / 2, cameraHeight / 2);

    // Ensure the spawn position is outside the camera view
    if (Random.value > 0.5f)
    {
        spawnX = (Random.value > 0.5f) ? spawnX + cameraWidth : spawnX - cameraWidth;
    }
    else
    {
        spawnY = (Random.value > 0.5f) ? spawnY + cameraHeight : spawnY - cameraHeight;
    }

    Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
    return spawnPosition;
}

public void UpdateScore(int points)
{
    playerScore += points;
}

public void TakeDamage(int damage)
{
    playerHealth -= damage;
    if (playerHealth <= 0)
    {
        GameOver();
    }
}

void GameOver()
{
    isGameOver = true;
    // Handle game over logic here
    Debug.Log("Game Over! Time's up.");
}
}