using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject cheesePrefab;


    [SerializeField] int speedEasy = 2;
    [SerializeField] int speedNormal = 4;
    [SerializeField] int speedHard = 5;
    [SerializeField] int speedHardCore = 6;

    [SerializeField] int healthEasy = 50;
    [SerializeField] int healthNormal = 100;
    [SerializeField] int healthHard = 150;
    [SerializeField] int healthHardCore = 200;

    [SerializeField] bool isGhost = false;

    private int health;
    private int speed;

    [SerializeField] int damage;
    [SerializeField] bool changeEnemyCount;

    [SerializeField] int minCheese, maxCheese;

    [SerializeField] bool boss;
    [SerializeField] GameObject slimePrefab;
    private AIPath aiPath;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();

        if (boss)
        {
            aiPath.canSearch = false;
            aiPath.maxSpeed = speedEasy;
        }
        else
        {

            if (Global.diff == 0)
            {
                speed = speedEasy;
                health = healthEasy;
            }
            else if (Global.diff == 1)
            {
                speed = speedNormal;
                health = healthNormal;
            }
            else if (Global.diff == 2)
            {
                speed = speedHard;
                health = healthHard;
            }
            else if (Global.diff == 3)
            {
                speed = speedHardCore;
                health = healthHardCore;
            }
            aiPath.maxSpeed = speed;
        }
    }

    private void Update()
    {
        if (Global.BossFightStart && boss)
        {
            aiPath.canSearch = true;
        }
        if (isGhost)
        {
            aiPath.canSearch = false;

            transform.position = Vector2.MoveTowards(transform.position, playermovement.playerTransform.position, speed * Time.fixedDeltaTime);

            float step = speed * Time.fixedDeltaTime;
            transform.position = Vector2.MoveTowards(transform.position, playermovement.playerTransform.position, step);
        }
        else
        {
            aiPath.canSearch = true;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
        {
            playerComponent.takeDamage(damage);
            if (isGhost)
            {
                health = 0;
            }
        }
    }

    public void takeDamage(int amount)
    {
        if (boss)
        {
            spawnCheese(-5, 2);
        }
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
            spawnCheese(minCheese, maxCheese);
        }
    }

    private void OnDestroy()
    {
        Global.killCount++;
        if (changeEnemyCount)
            Global.currentEnemies--;
    }

    public void spawnCheese(int low, int high)
    {
        int cheeseCount = Random.Range(low, high); // Generate a random number between 1 and 3

        if (cheeseCount > 0)
        {
            for (int i = 0; i < cheeseCount; i++)
            {
                // Generate a random position around the enemy's current position
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                Instantiate(cheesePrefab, spawnPosition, Quaternion.identity);
            }
        }
        else if (cheeseCount < -3 && Global.currentEnemies < 10)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            GameObject enemyObject = Instantiate(slimePrefab, spawnPosition, Quaternion.identity);

            AIDestinationSetter destinationSetter = GetComponent<AIDestinationSetter>();
            if (destinationSetter != null)
            {
                AIDestinationSetter enemyDestinationSetter = enemyObject.GetComponent<AIDestinationSetter>();
                if (enemyDestinationSetter != null)
                {
                    enemyDestinationSetter.target = destinationSetter.target; // Set the target of the enemy to the target of the Enemy class
                }
            }
        }
    }

}

