using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    Vector2 movement;

    Rigidbody2D rb;

    public ContactFilter2D movementFilter;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Cast a ray in the direction of movement
        int count = checkCollisions(movement);
        if (count == 0)
        {
            transform.Translate(movement * Time.deltaTime);
            return;
        }
        // If we hit something, try moving in the x direction
        count = checkCollisions(new Vector2(movement.x, 0));
        if (count == 0)
        {
            transform.Translate(new Vector2(movement.x, 0) * Time.deltaTime);
            return;
        }
        // If we hit something, try moving in the y direction
        count = checkCollisions(new Vector2(0, movement.y));
        if (count == 0)
        {
            transform.Translate(new Vector2(0, movement.y) * Time.deltaTime);
            return;
        }
    }

    int checkCollisions(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter, 
            castCollisions, 
            moveSpeed * Time.deltaTime + collisionOffset
        );
        return count;
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
    }
}

