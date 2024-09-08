using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float range;
    // public Transform target;
    private Transform target;
    private float minDistance = 5.0f;
    private bool targetCollision = false;
    private float speed = 2.0f;
    private float thrust = 2.0f;
    public int health = 5;

    SpriteRenderer spriteRenderer;
    Animator animator;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player2").transform;

        if (target == null)
        {
            Debug.Log("Player not found by Enemy script in Start.");
        }
        else{
             Debug.Log("Hello.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        range = Vector2.Distance(transform.position, target.position);
        if (range < minDistance)
        {
            if(!targetCollision)
            {
                // Get the position of the player
                transform.LookAt(target.position);

                // Correct the position
                transform.Rotate(new Vector3(0,-90,0), Space.Self);
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

                Vector3 direction = (target.position - transform.position).normalized;

                // // Flip the sprite based on the direction (if moving left)
                spriteRenderer.flipX = direction.x < 0;
            }

        }
        transform.rotation = Quaternion.identity;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !targetCollision)
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            targetCollision = true;

            bool right = contactPoint.x > center.x;
            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            bool bottom = contactPoint.y < center.y;

            if (right) GetComponent<Rigidbody2D>().AddForce(transform.right * thrust, ForceMode2D.Impulse);
            if (left) GetComponent<Rigidbody2D>().AddForce(-transform.right * thrust, ForceMode2D.Impulse);
            if (top) GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);
            if (bottom) GetComponent<Rigidbody2D>().AddForce(-transform.up * thrust, ForceMode2D.Impulse);
            Invoke("FalseCollision", 0.25f);

            print("Colliding with player");
        }

    }

    void FalseCollision()
    {
        targetCollision = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        Debug.Log("Enemy health: " + health);

        // Trigger the "hit" animation
        StartCoroutine(PlayHitAnimation());

        if (health <= 0)
        {
            Die();
        }

    } 

    private IEnumerator PlayHitAnimation()
    {
        // Set the "hit" trigger to play the animation
        animator.SetTrigger("hit");

        // Wait for the duration of the hit animation 
        yield return new WaitForSeconds(0.5f); 

    
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has been destroyed.");
        gameManager.SetEnemyCount(-1);
        Destroy(gameObject);
    } 




}

