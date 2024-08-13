using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject fizzle;

    private int maxDamage = 50;
    private int minDamage = 35;

    private float lifeTime = Global.bulletLifeTime;

    private void Start()
    {
        lifeTime = Global.bulletLifeTime;
    }


    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            if (fizzle != null)
            {
                GameObject effect = Instantiate(fizzle, transform.position, transform.rotation);
                Destroy(effect, 6f / 12f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            int damage = Random.Range(minDamage, maxDamage + 1);
            enemyComponent.takeDamage(damage);
        }


        if (projectile != null)
        {
            Destroy(projectile);

            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(effect, 10f / 13f);
            }
            else
            {
                Debug.Log("Hit effect is null");
            }
        }
        else
        {
            Debug.Log("Projectile is null");
        }
    }
}
