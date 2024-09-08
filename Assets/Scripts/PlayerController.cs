using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    Rigidbody2D rb;

    Animator animator;
    SpriteRenderer spriteRenderer;  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
{
    // Get the movement vector from the old input system
    float moveHorizontal = Input.GetAxisRaw("Horizontal");
    float moveVertical = Input.GetAxisRaw("Vertical");
    
    Vector2 movement = new Vector2(moveHorizontal, moveVertical);
    
    // Log the movement vector
    Debug.Log("Movement Vector: " + movement);
    
    if (movement == Vector2.zero)
    {
        animator.SetBool("isMoving", false);
        Debug.Log("Player is not moving");
    }
    else
    {
        animator.SetBool("isMoving", true);
        Debug.Log("Player is moving");

        // // Flip the sprite if we're moving left
        spriteRenderer.flipX = movement.x < 0;
        Debug.Log("Sprite flipped: " + spriteRenderer.flipX);
    }

    // Apply the velocity to the Rigidbody2D
    rb.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    
    // Log the velocity
    Debug.Log("Rigidbody Velocity: " + rb.velocity);
}

    // void Update()
    // {
        
    //     horizontal = Input.GetAxisRaw("Horizontal");
    //     vertical = Input.GetAxisRaw("Vertical");

    //     rb.velocity = new Vector2(horizontal * speed, vertical * speed);

    //     if (rb.velocity == Vector2.zero)
    //      {
    //         animator.SetBool("isMoving", false);
    //     }
    //     else
    //     {
    //         animator.SetBool("isMoving", true);
    //         // Flip the sprite if we're moving left
    //         spriteRenderer.flipX = movement.x < 0;
    //     }
    // }

    
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// {
//     Vector2 movement;

//     Rigidbody2D rb;

//     public ContactFilter2D movementFilter;
//     public float moveSpeed = 1f;
//     public float collisionOffset = 0.05f;
//     List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

//     Animator animator;
//     SpriteRenderer spriteRenderer;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//         spriteRenderer = GetComponent<SpriteRenderer>();
//     }

//     void FixedUpdate()
//     {
//          if (movement == Vector2.zero)
//         {
//             animator.SetBool("isMoving", false);
//         } else {
//         // Flip the sprite if we're moving left
//         spriteRenderer.flipX = movement.x < 0;
//         }
//         // Cast a ray in the direction of movement
//         int count = checkCollisions(movement);
//         if (count == 0)
//         {
//             transform.Translate(movement * Time.deltaTime);
//             return;
//         }
//         // If we hit something, try moving in the x direction
//         count = checkCollisions(new Vector2(movement.x, 0));
//         if (count == 0)
//         {
//             transform.Translate(new Vector2(movement.x, 0) * Time.deltaTime);
//             return;
//         }
//         // If we hit something, try moving in the y direction
//         count = checkCollisions(new Vector2(0, movement.y));
//         if (count == 0)
//         {
//             transform.Translate(new Vector2(0, movement.y) * Time.deltaTime);
//             return;
//         }
//     }

//     int checkCollisions(Vector2 direction)
//     {
//         int count = rb.Cast(
//             direction,
//             movementFilter, 
//             castCollisions, 
//             moveSpeed * Time.deltaTime + collisionOffset
//         );
//         return count;
//     }
//     void OnMove(InputValue movementValue)
//     {
//         animator.SetBool("isMoving", true);
//         movement = movementValue.Get<Vector2>() * 3;
//     }
// }
