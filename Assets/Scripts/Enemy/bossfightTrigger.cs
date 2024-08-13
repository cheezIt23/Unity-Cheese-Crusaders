using UnityEngine;

public class bossfightTrigger : MonoBehaviour
{
    private BoxCollider2D trigger;

    [SerializeField] private GameObject door;

    private void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
        {
            door.SetActive(true);
            Global.BossFightStart = true;
        }
        else
        {
            Debug.Log("not player tried to start boss fight.");
        }
    }


}
