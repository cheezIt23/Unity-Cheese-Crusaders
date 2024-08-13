using UnityEngine;


public class Global : MonoBehaviour
{
    public static int diff = -1;
    public static int mode = 0;
    public static int killCount = 0;
    public static int currentEnemies = 0; // current number of enemies

    //Player stats//
    public static int playerHealth = 100;
    public static int playerHealthMax = 100;
    /* public static int playerHealthMin = 100;*/

    //Movment//
    public static float playerSpeed = 5f;
    public static int rollDistance = 5;
    public static float rollCooldown = 1.5f;
    //Attack//
    public static float attackRate = 0.5f;
    public static float bulletLifeTime = 0.5f;



    public static int cheeseCount = 0;
    public static int upgradeCost = 20;

    public static bool BossFightStart = false;

    public static void reset()
    {
        mode = 0;
        killCount = 0;
        currentEnemies = 0; // current number of enemies
        cheeseCount = 0;
        BossFightStart = false;

        // Reset Upgrades //
        playerHealthMax = 100;
        
        playerSpeed = 5f;
        rollCooldown = 1.5f;

        //Attack//
        attackRate = 0.5f;
        bulletLifeTime = 0.5f;

        upgradeCost = 20;
    }
}