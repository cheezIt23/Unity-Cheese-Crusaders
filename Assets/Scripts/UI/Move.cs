using UnityEngine;

public class Move : MonoBehaviour
{
    private GameObject item;
    public int AmountY;
    public int AmountX;

    void OnCollisionEnter2D(Collision2D collision)
    {
        item = collision.gameObject;
        Vector3 newPosition = item.transform.position;
        newPosition.y += AmountY;
        newPosition.x += AmountX;

        item.transform.position = newPosition;
    }
}
