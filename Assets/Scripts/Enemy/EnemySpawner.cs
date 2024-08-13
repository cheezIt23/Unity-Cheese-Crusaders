using Pathfinding;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] int[] spawnChance;

    [SerializeField] float spawnInterval = 0.1f;
    [SerializeField] float decreaseTime = 0.001f;

    [SerializeField] float maxDistanceFromViewport = 5f;

    [SerializeField] int maxEnemies; //maximum number of enemies

    [SerializeField] bool RandomSpawnArea = true;

    [SerializeField] int fixedX, fixedY;

    [SerializeField] Transform player;


    private float timeUntilSpawn;

    private void Start()
    {
        timeUntilSpawn = spawnInterval;
    }

    private void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0 && maxEnemies > Global.currentEnemies)
        {
            spawnEnemy();

            timeUntilSpawn = spawnInterval;
        }
    }

    // spawns slime based on the index called num
    private void spawnEnemy()
    {
        int randomValue = Random.Range(0, 100);
        Debug.Log("randomValue: " + randomValue);
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            if (randomValue <= spawnChance[i])
            {
                // spawn enemy
                GameObject enemyObject = Instantiate(enemyPrefab[i], GenerateSpawnPosition(), Quaternion.identity);

                // set the target of the AIDestinationSetter script to the player
                AIDestinationSetter destinationSetter = enemyObject.GetComponent<AIDestinationSetter>();
                if (destinationSetter != null)
                {
                    destinationSetter.target = player;
                }
                if (spawnInterval > 0)
                {
                    spawnInterval -= decreaseTime;
                }

                Global.currentEnemies++; // increment count
                break; // exit the loop once an enemy is spawned
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool isValidPosition = false;
        int attempts = 0;
        int maxAttempts = 100; // Set a limit to the number of attempts

        var cam = Camera.main;
        var radius = cam.orthographicSize * 2f;
        var cameraPos = cam.transform.position;

        // Random normalized position within a circle
        var normalizedRandomPos = Random.insideUnitCircle.normalized;

        // Random distance outside of circle
        var distance = Random.Range(radius, radius + maxDistanceFromViewport);

        // Multiplying to get values outside of circle, and adjusting position
        spawnPosition = (Vector3)normalizedRandomPos * distance + cameraPos;

        while (!IsSpawnPositionValid(spawnPosition) && attempts < maxAttempts)
        {
            if (RandomSpawnArea)
            {
                cam = Camera.main;
                radius = cam.orthographicSize * 2f;
                cameraPos = cam.transform.position;

                // Random normalized position within a circle
                normalizedRandomPos = Random.insideUnitCircle.normalized;

                // Random distance outside of circle
                distance = Random.Range(radius, radius + maxDistanceFromViewport);

                // Multiplying to get values outside of circle, and adjusting position
                spawnPosition = (Vector3)normalizedRandomPos * distance + cameraPos;
            }
            else
            {
                // Fixed position
                spawnPosition = new Vector3(fixedX, fixedY, 0);
            }

            attempts++; // Increment the number of attempts
        }

        if (!IsSpawnPositionValid(spawnPosition))
        {
            Debug.LogError("Failed to find a valid spawn position after " + maxAttempts + " attempts.");
            // Handle the failure case, e.g., by returning a default position or not spawning an enemy
        }

        return spawnPosition;
    }

    private bool IsSpawnPositionValid(Vector3 position)
    {
        // Create a new Physics.RaycastHit to store the hit information
        RaycastHit2D hit;

        // Define the range of the raycast
        float range = 1f; // Adjust this value to your needs

        LayerMask obstacleLayerMask = LayerMask.GetMask("Obstacle");

        // Cast a ray from the spawn position in all directions
        hit = Physics2D.Raycast(position, Vector2.up, range, obstacleLayerMask);
        Debug.DrawRay(position, Vector2.up * range, Color.red); // Visualize the raycast
        if (hit.collider != null)
        {
            return false;
        }

        hit = Physics2D.Raycast(position, Vector2.down, range, obstacleLayerMask);
        Debug.DrawRay(position, Vector2.down * range, Color.red); // Visualize the raycast
        if (hit.collider != null)
        {
            return false;
        }

        hit = Physics2D.Raycast(position, Vector2.left, range, obstacleLayerMask);
        Debug.DrawRay(position, Vector2.left * range, Color.red); // Visualize the raycast
        if (hit.collider != null)
        {
            return false;
        }

        hit = Physics2D.Raycast(position, Vector2.right, range, obstacleLayerMask);
        Debug.DrawRay(position, Vector2.right * range, Color.red); // Visualize the raycast
        if (hit.collider != null)
        {
            return false;
        }

        // Cast a ray from the spawn position in diagonal directions
        hit = Physics2D.Raycast(position, Vector2.up + Vector2.right, range, obstacleLayerMask);
        Debug.DrawRay(position, (Vector2.up + Vector2.right) * range, Color.red); // Visualize the raycast
        if (hit.collider != null)
        {
            return false;
        }

        hit = Physics2D.Raycast(position, Vector2.up + Vector2.left, range, obstacleLayerMask);
        Debug.DrawRay(position, (Vector2.up + Vector2.left) * range, Color.red); // Visualize the raycast
        if (hit.collider != null)
        {
            return false;
        }

        hit = Physics2D.Raycast(position, Vector2.down + Vector2.right, range, obstacleLayerMask);
        Debug.DrawRay(position, (Vector2.down + Vector2.right) * range, Color.red); // Visualize the raycast
        if (hit.collider != null)
        {
            return false;
        }

        hit = Physics2D.Raycast(position, Vector2.down + Vector2.left, range, obstacleLayerMask);
        Debug.DrawRay(position, (Vector2.down + Vector2.left) * range, Color.red); // Visualize the raycast
        if (hit.collider != null)
        {
            return false;
        }

        // If no colliders were hit, the position is valid
        return true;
    }

}
