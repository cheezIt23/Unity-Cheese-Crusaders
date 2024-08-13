using TMPro;
using UnityEngine;

public class UpdateTextUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;

    [SerializeField] string type;

    [SerializeField] bool changeColor = false;

    [SerializeField] string hexColor1;
    [SerializeField] string hexColor2;

    // Update is called once per frame
    void Update()
    {
        if (type == "killCount")
        {
            countText.text = Global.killCount + "";
        }
        else if (type == "playerHealth")
        {
            if (Global.playerHealthMax > 0)
            {
                countText.text = Global.playerHealth + "/" + Global.playerHealthMax;
            }
            if (Global.playerHealthMax < 0)
            {
                countText.text = Global.playerHealth + "/0";

            }
        }
        else if (type == "cheeseCount")
        {
            countText.text = Global.cheeseCount + "";
        }
        else if (type == "upgradeCount")
        {
            countText.text = Global.upgradeCost + "";
        }

        if (changeColor)
        {
            if (Global.cheeseCount >= Global.upgradeCost)
            {
                // Convert hexColor2 to Color and apply it
                countText.color = HexToColor(hexColor2);
            }
            else
            {
                // Convert hexColor1 to Color and apply it
                countText.color = HexToColor(hexColor1);
            }
        }
    }

    // Helper method to convert hex color string to Color
    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}

