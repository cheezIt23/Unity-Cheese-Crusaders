using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    private float healthValue;

    [SerializeField] private GameObject hurtBox;
    [SerializeField] private float invincableTime;

    private float time;

    private int cheeseHealPrice;

    [SerializeField] private GameObject deathScreen;

    private void Start()
    {
        if (Global.diff == -1)
        {
            Global.playerHealth = 75;
            Global.playerHealthMax = 100;
            cheeseHealPrice = 2;
        }

        else if (Global.diff == 0)
        {
            Global.playerHealthMax = 150;
            cheeseHealPrice = 2;
        }

        else if (Global.diff == 1)
        {
            Global.playerHealthMax = 100;
            cheeseHealPrice = 2;

        }
        else if (Global.diff == 2)
        {
            Global.playerHealthMax = 50;
            cheeseHealPrice = 5;
        }

        else if (Global.diff == 3)
        {
            Global.playerHealthMax = 25;
            cheeseHealPrice = 20;
        }

        if (Global.diff != -1)
            Global.playerHealth = Global.playerHealthMax;

        StartCoroutine(updateHealthBar());
    }

    private void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0)
        {
            hurtBox.SetActive(true);
        }

        if (Input.GetButton("Fire2"))
        {
            if (Global.playerHealth <= Global.playerHealthMax - 1 && Global.cheeseCount >= cheeseHealPrice)
            {
                Global.playerHealth += 1;
                Global.cheeseCount -= cheeseHealPrice;

                StartCoroutine(updateHealthBar());
            }

        }
    }

    public void DisableHurtBox(float time)
    {
        hurtBox.SetActive(false);

        this.time = time;
    }

    public void takeDamage(int damage)
    {
        Global.playerHealth -= damage;
        Debug.Log(damage + " damage taken");

        hurtBox.SetActive(false);

        if (Global.playerHealth <= 0)
        {

            Global.playerHealth = 0;
            healthBar.value = 0;
            Time.timeScale = 0; // Pause the game
            deathScreen.SetActive(true);
        }
        else if (Global.playerHealth > 0)
        {
            ScreenShake.Instance.ShakeCamera(5f, 0.1f);
            time = invincableTime;

            if (healthBar != null)
            {
                StartCoroutine(updateHealthBar());
            }
        }
    }

    public void changeHealth()
    {
        StartCoroutine(updateHealthBar());
    }

    IEnumerator updateHealthBar()
    {
        healthValue = (float)Global.playerHealth / Global.playerHealthMax;

        float startValue = healthBar.value;
        float elapsed = 0;

        while (elapsed < 1)
        {
            healthBar.value = Mathf.Lerp(startValue, healthValue, elapsed);
            elapsed += Time.deltaTime * 3; // the rate of change
            yield return null;
        }

        healthBar.value = healthValue;
    }
}
