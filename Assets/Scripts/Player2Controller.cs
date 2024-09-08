using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    Rigidbody2D rb;

    Animator animator;
    SpriteRenderer spriteRenderer;  

    public bool canMove = true;

    public SwordAttack swordAttack;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private bool isDead = false;

   

    public Text mainTitleText;
    public Image introOverlay;

    public Text gameOverText;
    public Image gameOverOverlay;

    public Text instructionText;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //Initialize Start Screen
        mainTitleText.gameObject.SetActive(true);
        introOverlay.gameObject.SetActive(true);
        Invoke("HideStartScreen",2);
    }

    void Update()
    {
        if (!canMove) return;

        // Get the movement vector from the old input system
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Handle attack input
        if (Input.GetButtonDown("Fire1"))
        {
            OnFire();
        }
    }

    void FixedUpdate()
    {
        if (!canMove) 
        {
            rb.velocity = Vector2.zero; // Ensure the player stops moving when canMove is false
            return;
        }

        Vector2 movement = new Vector2(horizontal, vertical);

        if (movement == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
            spriteRenderer.flipX = movement.x < 0;
        }

        // Apply the velocity to the Rigidbody2D
        rb.velocity = movement * speed;

        // Lock movement if health is < 1
        if (currentHealth < 1 && !isDead)
        {
            LockMovement();
        }
    }

    void OnFire()
    {
        animator.SetTrigger("attack");
        LockMovement();  // Ensure movement is locked when attacking
        Debug.Log("Player is attacking");

    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void SwordAttack()
    {
        LockMovement();
        if (spriteRenderer.flipX)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void StopAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 5;
            healthBar.SetHealth(currentHealth);
            if (currentHealth < 1 && !isDead)
            {
                 isDead = true;
                 animator.SetBool("isDead", true);
                 gameOverText.gameObject.SetActive(true);
                 gameOverOverlay.gameObject.SetActive(true);

                 // Disable movement when HP hits 0
                 LockMovement();
                 rb.velocity = Vector2.zero;  // Stop all movement immediately
            }
        }
        else if (collision.gameObject.CompareTag("Spawner"))
        {
            collision.gameObject.GetComponent<Spawner>().GetGateway();
            Debug.Log("Player enters spawner");
        }

    }

    void HideStartScreen()
    {
        mainTitleText.gameObject.SetActive(false);
        introOverlay.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(true);
    }

   
}
