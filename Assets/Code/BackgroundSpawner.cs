using UnityEngine;
using System.Collections.Generic;

public class BackgroundSpawner : MonoBehaviour
{
    [Header("Background Tile Settings")]
    [Tooltip("The background prefab that contains a SpriteRenderer with your tileable image.")]
    public GameObject backgroundPrefab;

    [Tooltip("The width (and height) of one background tile (in world units).")]
    public float tileSize = 10f;

    [Tooltip("How many tiles around the camera should be active (in each direction).")]
    public int renderDistance = 2;

    private Dictionary<Vector2Int, GameObject> spawnedTiles = new Dictionary<Vector2Int, GameObject>();
    private Transform cameraTransform;

    void Start()
    {
        // Get a reference to the main camera's transform.
        cameraTransform = Camera.main.transform;
        // Initial population of background tiles.
        UpdateTiles();
    }

    void Update()
    {
        // Check and update background tiles each frame.
        UpdateTiles();
    }

    void UpdateTiles()
    {
        // Calculate the camera's current tile coordinate.
        Vector2 cameraPos = cameraTransform.position;
        Vector2Int cameraTile = new Vector2Int(
            Mathf.RoundToInt(cameraPos.x / tileSize),
            Mathf.RoundToInt(cameraPos.y / tileSize)
        );

        // Loop through the grid within the renderDistance.
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int tileCoord = new Vector2Int(cameraTile.x + x, cameraTile.y + y);
                // If the tile doesn't exist yet, spawn it.
                if (!spawnedTiles.ContainsKey(tileCoord))
                {
                    Vector3 tilePos = new Vector3(tileCoord.x * tileSize, tileCoord.y * tileSize, 0f); // z at 10 to render behind gameplay
                    GameObject tile = Instantiate(backgroundPrefab, tilePos, Quaternion.identity, transform);
                    spawnedTiles.Add(tileCoord, tile);
                }
            }
        }

        // Optional: Remove tiles that are far from the camera to keep things tidy.
        List<Vector2Int> keysToRemove = new List<Vector2Int>();
        foreach (var kvp in spawnedTiles)
        {
            Vector2 tileCenter = new Vector2(kvp.Key.x * tileSize, kvp.Key.y * tileSize);
            if (Vector2.Distance(tileCenter, cameraPos) > (renderDistance + 1) * tileSize)
            {
                keysToRemove.Add(kvp.Key);
                Destroy(kvp.Value);
            }
        }
        foreach (var key in keysToRemove)
        {
            spawnedTiles.Remove(key);
        }
    }
}
