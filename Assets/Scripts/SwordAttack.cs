// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SwordAttack : MonoBehaviour
// {
//     public Collider2D swordCollider;
//     Vector2 rightAttackOffset;


//     void Start()
//     {
//         swordCollider = GetComponent<Collider2D>();
//         rightAttackOffset = transform.position;

//         swordCollider.enabled = true; // New

//     }

//     public void AttackLeft()
//     {
//         print("Attack Left!");
//         swordCollider.enabled = true;
//         transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);

        
//     }

//     public void AttackRight()
//     {
//         print("Attack Right!");
//         swordCollider.enabled = true;
//         transform.localPosition = rightAttackOffset;

//     }

// 	public void StopAttack()
//     {
//         // swordCollider.enabled = false;
//         swordCollider.enabled = true;
//     }

//     public void OnTriggerEnter2D(Collider2D collision)
//     {
//        if (collision.tag == "Enemy")
//         {
//             print("Attacking enemy");
//         }
//     }

   
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SwordAttack : MonoBehaviour
// {
//     public Collider2D swordCollider;
//     private Vector2 rightAttackOffset;

//     void Start()
//     {
//         swordCollider = GetComponent<Collider2D>();
//         rightAttackOffset = transform.localPosition;

//         // Initially disable the sword collider
//         swordCollider.enabled = false;
//     }

//     public void AttackLeft()
//     {
//         Debug.Log("Attack Left!");
//         transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
//         swordCollider.enabled = true;  // Enable the collider when attacking
//     }

//     public void AttackRight()
//     {
//         Debug.Log("Attack Right!");
//         transform.localPosition = rightAttackOffset;
//         swordCollider.enabled = true;  // Enable the collider when attacking
//     }

//     public void StopAttack()
//     {
//         Debug.Log("Attack Stopped");
//         swordCollider.enabled = false;  // Disable the collider after the attack
//     }

//     void OnTriggerEnter2D(Collider2D collision)
//     {
//         Debug.Log("Collision detected with " + collision.name);  // Check what the sword is colliding with

//         if (collision.CompareTag("Enemy"))
//         {
//             Debug.Log("Attacking enemy: " + collision.name);
            
//              // Access the Enemy script to apply damage
//             Enemy enemy = collision.GetComponent<Enemy>();
//             if (enemy != null)
//             {
//                 enemy.TakeDamage(1);  // Apply 1 damage (or any amount you choose)
//             }
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    private Vector2 rightAttackOffset;

    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.localPosition;

        // Initially disable the sword collider
        swordCollider.enabled = false;
    }

    public void AttackLeft()
    {
        Debug.Log("Attack Left!");
        transform.localPosition = new Vector3(-rightAttackOffset.x, rightAttackOffset.y);
        swordCollider.enabled = true;

        Debug.Log("Sword Position (Left): " + transform.localPosition);
        Debug.Log("Sword Collider Enabled: " + swordCollider.enabled);
    }

    public void AttackRight()
    {
        Debug.Log("Attack Right!");
        transform.localPosition = rightAttackOffset;
        swordCollider.enabled = true;

        Debug.Log("Sword Position (Right): " + transform.localPosition);
        Debug.Log("Sword Collider Enabled: " + swordCollider.enabled);
    }

    public void StopAttack()
    {
        Debug.Log("Attack Stopped");
        swordCollider.enabled = false;  // Disable the collider after the attack
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision detected with {collision.name} at position {collision.transform.position}");

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log($"Attacking enemy: {collision.name}");
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }
        else if (collision.CompareTag("Spawner"))
        {
            Debug.Log($"Attacking spawner: {collision.name}");
            Spawner spawner = collision.GetComponent<Spawner>();
            if (spawner != null)
            {
                spawner.TakeDamage(1);
            }
        }
    }
}
