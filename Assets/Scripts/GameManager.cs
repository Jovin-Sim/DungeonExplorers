using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawners;
    private int level = 0;
    private int currentScene = 0;
    private int enemyCount = 0;
    public int enemyLimit = 10;

    public GameObject player;
    public GameObject hudCanvas;

    private Scene scene;

    public Text instructionText;

    // public AudioSource audioSource; // Reference to the AudioSource
    
    // Start is called before the first frame update
    void Start()
    {
        PrepareSpawners();

        // // Play audio if it exists
        // if (audioSource != null)
        // {
        //     audioSource.Play();
        // }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (instructionText != null)
        {
            if (level > 0)
            {
                instructionText.text = "Destroy the correct statues to advance";
            }
        }
        else
        {
            Debug.LogError("InstructionText is not assigned in the Inspector.");
        }

    }

    void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(player.gameObject);
        DontDestroyOnLoad(hudCanvas.gameObject);
        DontDestroyOnLoad(gameObject);

        // // Ensure the AudioSource persists as well
        // if (audioSource != null)
        // {
        //     DontDestroyOnLoad(audioSource.gameObject);
        // }
        
        scene = SceneManager.GetActiveScene();


    }

     void PrepareSpawners()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        if(spawners.Length > 0)
        {
            int rnd = Random.Range(0, spawners.Length);
            spawners[rnd].GetComponent<Spawner>().SetGateway(true);
            // // Weapon Upgrade testing
            // if(Random.Range(0, 5) == 3)
            // {
            //     int randTemp = Random.Range(0, spawners.Length);
            //     spawners[randTemp].GetComponent<SpawnerScript>().SetWeapon(true);
            // }
            foreach (GameObject spawner in spawners)
            {
                spawner.GetComponent<Spawner>().SetHealth(level + Random.Range(3, 6));
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign instructionText after scene change
        instructionText = GameObject.Find("Instruction1")?.GetComponent<Text>();  

        if(!string.Equals(scene.path, this.scene.path)){
            level++;
            PrepareSpawners();
        }
    }

    public void SetEnemyCount(int amount)
    {
        enemyCount += amount;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public int GetEnemyLimit()
    {
        return enemyLimit;
    }

    public void LoadLevel()
    {
        enemyCount = 0;
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            currentScene = 1;
        }
        else 
        {
            currentScene = -1;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + currentScene);

    }
}
