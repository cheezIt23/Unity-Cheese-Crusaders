using UnityEngine;


public class playermovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    private float rollCooldownTimer = 0f; // Instance variable for cooldown timer

    [SerializeField] private GameObject HurtBox;

    [SerializeField] public static Transform playerTransform;

    [SerializeField] private PlayerHealth PHS;

    [SerializeField] private GameObject RollEffect;

    [SerializeField] private GameObject upgradeScreen;



    // Update is called once per frame
    void Update()
    {
        // get the movement input //
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", movement.magnitude);

        // Update cooldown timer
        if (rollCooldownTimer > 0)
        {
            rollCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            upgradeScreen.SetActive(!upgradeScreen.activeSelf);
            if (upgradeScreen.active == true) {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            

        }

        if (Input.GetKeyDown(KeyCode.Space) && rollCooldownTimer <= 0)
        {
            rollCooldownTimer = Global.rollCooldown;
            Roll();
        }

        /*if (Input.GetKey(KeyCode.KeypadPlus))
        {
            Global.cheeseCount += 1;
        }*/
    }

    void FixedUpdate()
    {


        if (Global.playerHealth > 0)
        {


            if(rollCooldownTimer < Global.rollCooldown - 0.1f)
            {
                rb.MovePosition(rb.position + movement * Global.playerSpeed * Time.fixedDeltaTime);
                playerTransform = transform;
            }
        }
    }
    void Roll()
    {
        PHS.DisableHurtBox(1);
        // Roll logic here
        rb.velocity = movement * Global.playerSpeed * Global.rollDistance;

        // Instantiate the RollEffect 
        GameObject rollEffectInstance = Instantiate(RollEffect, transform.position, Quaternion.identity);

        Invoke("InstantiateSecondRollEffect", 0.1f);
    }

    void InstantiateSecondRollEffect()
    {
        // Instantiate the second RollEffect at the same position as the first one
        GameObject secondRollEffectInstance = Instantiate(RollEffect, transform.position, Quaternion.identity);
    }


}

