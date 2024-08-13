using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    [SerializeField] List<string> upgrades = new List<string>();
    [SerializeField] TextMeshProUGUI upgradeText;
    private PlayerHealth ph;
    int rerollTries = 0;

    private void Start()
    {
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public void getRandomEffect()
    {
        if (Global.cheeseCount >= Global.upgradeCost)
        {
            Global.cheeseCount -= Global.upgradeCost;
            int index = Random.Range(0, upgrades.Count);
            string effect = upgrades[index];
            changeEffect(effect);
        }
        else
        {
            upgradeText.text = "You Need More Cheese";

            return;
        }
    }

    private void changeEffect(string effect)
    {
        int amount = Random.Range(-6, 8);

        if (effect.Equals("playerHealthMax"))
        {
            Global.playerHealthMax += amount * 4;

            if (Global.playerHealth > Global.playerHealthMax)
            {
                int damage = Global.playerHealth - Global.playerHealthMax;
                ph.takeDamage(damage);
            }

            if (amount > 0)
            {
                ph.changeHealth();
                upgradeText.text = "Your Max Health Has Been Changed By " + (amount * 4);
            }
            else if (amount < 0)
            {
                upgradeText.text = "Your Max Health Has Been Changed By " + (amount * 4);
            }
        }

        if (effect.Equals("upgradeCost"))
        {

            if (Global.upgradeCost + (amount * 2) > 5)
            {
                Global.upgradeCost += amount;
            }
            else
            {
                Global.upgradeCost = 5;
            }
            upgradeText.text = "The Upgrade Cost Is Now " + (Global.upgradeCost);
        }

        if (effect.Equals("bulletLifeTime"))
        {
         

            Global.bulletLifeTime += (float)(amount / 100f);

            if (amount > 0)
            {
                upgradeText.text = "Your Spells Will Now Travel Farther";
            }

            else if (amount < 0)
            {
                upgradeText.text = "Your Spells Will Not Travel As Far";
            }
        }

        if (effect.Equals("playerSpeed"))
        {
            while (Global.playerSpeed + amount / 10f > 7f)
            {
                if(rerollTries >= 100)
                {
                    Debug.LogError("Max amount of rerolls player gets nothing");
                    rerollTries = 0;
                    upgradeText.text = "You Received Nothing. Womp Womp";
                    return;

                }
                amount = Random.Range(-6, 8);
                rerollTries++;

            }
            
                Global.playerSpeed += amount / 10f;

                if (amount > 0)
                {
                    upgradeText.text = "You Will Move Faster Now";
                }

                else if (amount < 0)
                {
                    upgradeText.text = "You Will Move Slower Now";

                }
            
        }

        if (effect.Equals("rollCooldown"))
        {
            while(Global.rollCooldown + amount / 10f <= 1.1f)
            {
                if (rerollTries >= 100)
                {
                    Debug.LogError("Max amount of rerolls player gets nothing");
                    rerollTries = 0;
                    upgradeText.text = "You Received Nothing. Womp Womp";
                    return;

                }
                amount = Random.Range(-6, 8);
                rerollTries++;
            }
            Global.rollCooldown += amount / 10f;
            if (amount < 0)
            {
                upgradeText.text = "Your Roll Cooldown Was Shortened";
            }

            else if (amount > 0)
            {
                upgradeText.text = "Your Roll Cooldown Is Now Longer";

            }
        }

        if (effect.Equals("attackRate"))
        {
            while (Global.attackRate + amount / 10f <= 0.2f)
            {
                if (rerollTries >= 100)
                {
                    Debug.LogError("Max amount of rerolls player gets nothing");
                    rerollTries = 0;
                    upgradeText.text = "You Received Nothing. Womp Womp";
                    return;

                }
                amount = Random.Range(-6, 8);
                rerollTries++;
            }

            Global.attackRate += amount / 10f;

            if (amount < 0)
            {
                upgradeText.text = "You Can Now Shoot Faster";
            }

            else if (amount > 0)
            {
                upgradeText.text = "You Now Shoot Slower";

            }
        }



        if (amount == 0)
        {
            upgradeText.text = "You Received Nothing. Womp Womp";
            
        }

    }
}
