using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float shootCooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Global.playerHealth > 0 && Time.deltaTime != 0)
        {
            if (Input.GetButton("Fire1") && shootCooldown <= 0)
            {
                Shoot(1);
                shootCooldown = Global.attackRate;
            }

            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
        }
    }

    void Shoot(int type)
    {
        if (type == 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        }


    }


}
