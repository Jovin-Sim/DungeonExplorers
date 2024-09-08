using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] spawnPoints;
    private float timer;
    private int spawnIndex = 0;
    public int health = 5;
    public Sprite deathSprite;
    public Sprite gateway;

    public Collider2D spawnerCollider;

    public GameObject gatewayPrefab;

    // public Sprite[] sprites;

    public GameObject[] objects; // Array to hold the GameObjects

    private GameObject selectedObject; // Variable to store the randomly selected object

    public bool isGateway = false;

    private GameManager gameManager;

    public bool isGatewayOpen = false;

    // Get the SpriteRenderer component of the Spawner
    SpriteRenderer spawnerSpriteRenderer; 
        

    void Start()
    {
        spawnerSpriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InstantiateEnemyAt(spawnPoints[0]);
        InstantiateEnemyAt(spawnPoints[1]);
        timer = Time.time + 7.0f;
        gameManager.SetEnemyCount(2);


        if (spawnerCollider == null)
        {
            spawnerCollider = GetComponent<Collider2D>();
        }
        if (spawnerCollider != null)
        {
            spawnerCollider.enabled = true; // Ensure the collider is enabled at the start
        }
        
    }

    void Update()
    {
     
        if (timer < Time.time && gameManager.GetEnemyCount() < gameManager.GetEnemyLimit())
        {
            InstantiateEnemyAt(spawnPoints[spawnIndex % 2]);
            timer = Time.time + 7.0f;
            spawnIndex++;
            gameManager.SetEnemyCount(1);
        }
         if (spawnerCollider != null)
        {
            spawnerCollider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colliding with player");
            // Add damage or other logic here
        }
    }

    void InstantiateEnemyAt(GameObject spawnPoint)
    {
        if (enemy != null)
        {
            Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Enemy reference is null. Cannot instantiate.");
        }
    }

    public void TakeDamage(int amount)
    {
        if(GetComponent<SpriteRenderer>().sprite != gateway)
        {
            health -= amount;
            GetComponent<SpriteRenderer>().color = Color.red;
            if(health <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = deathSprite;
                if (isGateway)
                {
                    Invoke("OpenGateway", 0.5f);
                } else
                {
                    Destroy(gameObject);
                }
            }
            Invoke("DefaultColor", 0.3f);
        }

    }

    private void OpenGateway()
    {
        // // Instantiate the gateway prefab at the spawner's position with no rotation
        // Instantiate(gatewayPrefab, transform.position, Quaternion.identity);
        // Destroy(gameObject);
        // isGatewayOpen = true;
       
        // Get the SpriteRenderer component of the gatewayPrefab
        SpriteRenderer gatewaySpriteRenderer = gatewayPrefab.GetComponent<SpriteRenderer>();
        
        // Change the sprite of the Spawner to match that of the gatewayPrefab
        spawnerSpriteRenderer.sprite = gateway;

        transform.localScale = new Vector3(0.5f, 0.5f, 1f); // Scale down by 50%

        // Change sorting layer so player can enter
        spawnerSpriteRenderer.sortingLayerName = "Spawner"; // Change to your desired sorting layer
        spawnerSpriteRenderer.sortingOrder = 1;
        
        // // Destroy the Spawner object
        // Destroy(gameObject);

        Destroy(gameObject.transform.GetChild(0).gameObject);
        Destroy(gameObject.transform.GetChild(1).gameObject);
        
        Debug.Log("Gateway changed");

    }

    private void DefaultColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public void SetGateway(bool maybe)
    {
        isGateway = maybe;
        Debug.Log("Gateway: " + gameObject.name);
    }

    public void GetGateway()
    {
        // return GetComponent<SpriteRenderer>().sprite == gateway;
        
        // Access the SpriteRenderer of the gatewayPrefab and get its sprite
        // Sprite gatewaySprite = gatewayPrefab.GetComponent<SpriteRenderer>().sprite;

        // // Compare the current sprite of the spawner with the sprite from the gatewayPrefab
        // return GetComponent<SpriteRenderer>().sprite == gatewaySprite;
        // Debug.Log("Gateway collided! ");

        // Sprite gatewaySprite = gatewayPrefab.GetComponent<SpriteRenderer>().sprite;

        // if(GetComponent<SpriteRenderer>().sprite == gatewaySprite)
        // {
        //     gameManager.LoadLevel();
        // }
        

        if(GetComponent<SpriteRenderer>().sprite == gateway)
        {
            gameManager.LoadLevel();
            Debug.Log("gateway open");
        }
        else{
            Debug.Log("gateway closed");
        }

        // Sprite gatewaySprite = gatewayPrefab.GetComponent<SpriteRenderer>().sprite;
        // Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;

        // Debug.Log("Current Sprite: " + currentSprite.name);
        // Debug.Log("Gateway Sprite: " + gatewaySprite.name);

        // if (currentSprite == gatewaySprite)
        // {
        //     Debug.Log("Sprites match! Loading next level...");
        //     gameManager.LoadLevel();
        // }
        // else
        // {
        //     Debug.Log("Sprites do not match. No level load.");
        // }
    }
}
