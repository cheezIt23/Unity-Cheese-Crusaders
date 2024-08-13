using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    private Transform player; // Reference to the player object
    [SerializeField] private float speed = 1.0f; // Speed at which the cheese moves towards the player

    // Start is called before the first frame update
    void Start()
    {
        SetRandomSprite();

        SetRandomSprite();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);

        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);

        if (Vector2.Distance(transform.position, player.position) < 0.01f) // Check if the distance between the cheese and the player is less than a small threshold
        {
            Destroy(gameObject); // Destroy the cheese object
        }
    }

    private void OnDestroy()
    {
        Global.cheeseCount++;
    }

    void SetRandomSprite()
    {
        spriteRenderer.sprite = GetRandomSprite();
    }

    // Get a random sprite from the array
    Sprite GetRandomSprite()
    {
        int randomIndex = Random.Range(0, sprites.Count);
        return sprites[randomIndex];
    }

}

