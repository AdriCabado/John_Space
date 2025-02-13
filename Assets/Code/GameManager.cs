using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerHealth = 100;
    public int playerScore = 0;
    public bool isGameOver = false;

    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnRate = 2f;
    private float nextSpawnTime = 0f;

    public float gameDuration = 600f; // 10 minutes in seconds
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
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
        }
    }

    void StartGame()
    {
        playerHealth = 100;
        playerScore = 0;
        isGameOver = false;
        elapsedTime = 0f;
        nextSpawnTime = Time.time + spawnRate;
    }

    void UpdateGame()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }

        // Additional game update logic here
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
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
