using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] Collider2D fov;

    [SerializeField] AIPath aiPath;
    private PlayerHealth playerComponent;

    private void Start()
    {
        
        aiPath.canSearch = false;
    }

/*    private void Update()
    {
        if (playerComponent != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerComponent.transform.position);
            float detectionRadius = fov.bounds.extents.magnitude * 2;
            aiPath.canSearch = distanceToPlayer <= detectionRadius;
            
        }
    }*/


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D called");
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out playerComponent))
        {
            
            aiPath.canSearch = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
        {
            aiPath.canSearch = false;
        }
    }
}
