using Pathfinding;
using UnityEngine;

public class EnemyGFX : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private float horizontal;
    private float vertical;

    [SerializeField] private AIPath aiPath;

    void Update()
    {
        if (aiPath != null && aiPath.remainingDistance > 0)
        {
            Vector3 direction = aiPath.desiredVelocity.normalized;

            horizontal = direction.x;
            vertical = direction.y;

            if (animator != null)
            {
                animator.SetFloat("Horizontal", horizontal);
                animator.SetFloat("Vertical", vertical);
            }
        }
    }

}
