using UnityEngine;
using UnityEngine.UI;

public class RandListImage : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Color[] slimeColors; // New field for slime colors
    private int spriteCount;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        spriteCount = sprites.Length;

        // Get all the Image components of the children
        Image[] images = GetComponentsInChildren<Image>();

        // Assign a random sprite to each Image component, except for the one named "Mover"
        foreach (Image image in images)
        {   
            if (image.gameObject.name != "Mover")
            {
                Sprite randomSprite = GetRandomSprite();
                image.sprite = randomSprite;

                if (index == 0)
                {
                    Color randomColor = GetRandomColor();
                    image.color = randomColor;
                }
            }
        }
    }

    // Get a random sprite from the array
    Sprite GetRandomSprite()
    {
        int randomIndex = Random.Range(0, spriteCount);
        index = randomIndex;
        return sprites[randomIndex];
    }

    // Get a random color from the slimeColors array
    Color GetRandomColor()
    {
        int randomIndex = Random.Range(0, slimeColors.Length);
        return slimeColors[randomIndex];
    }
}
