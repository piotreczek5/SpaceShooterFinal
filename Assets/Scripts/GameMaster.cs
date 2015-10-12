﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public Vector3 boundry;                             // area where ships can move


    [HideInInspector]
    public Transform hierarchyGuard;                    // to keep all created (Clone) in one Transform
    public GameObject playerHolder;

    [Header("UI")]
    [HideInInspector]
    public Text scoreText;                            // player score label on screen
    public Text lifesText;
    public int score;
    public int lifes = 3;



    [Header("Enemy spawn settings")]
    public bool isEnemySpawn = true;
    public GameObject[] enemies;
    [Tooltip("Place where enemy will be spawn")]
    public Vector3 enemyPosition;
    [Tooltip("Time between spawning enemy")]
    public float enemySpawnTime = 3;

    [Header("Enemy drop settings")]
    public GameObject[] dropItems;
    [Range(0, 100)]
    public int dropPercent;


    [Header("PickUp spawn settings")]
    public bool isPickUpSpawn = true;
    public GameObject[] pickUps;
    [Tooltip("Place where pickUp will be spawn")]
    public Vector3 pickUpPosition;
    [Tooltip("Time between spawning pickUp")]
    public float pickUpTime = 3;

    [Header("Fuel")]
    public GameObject fuelSpawn;
    public float fuelSpawnTime;

    [Header("Ammo")]
    public GameObject ammo;
    public float ammoSpawnTime;

    [Space(5)]
    public int goldenScore = 100;               // max score value, when label change his color to gold for a while
    private int nextGoldenScore;                // nextGoldenScore is sum of all previous goldenScores
    private bool playerAlive;

    private const string scoreAnimation = "GoldLabel";

    private BulletTime bulletTime;             // reference to BulletTime script


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(lifesText);
        DontDestroyOnLoad(scoreText);

        SpawnPlayer();
        nextGoldenScore = goldenScore;           //  initial first golden Score
        GetGUIReferences();                      // ger references to GUI components
        //lifesText.text = "Lifes: " + lifes;
        scoreText.text = "Score: 0";

        bulletTime = GetComponent<BulletTime>();

        hierarchyGuard = new GameObject("HierarchyGuard").transform;

        if (isEnemySpawn)
            StartCoroutine(RandomObjectSpawner(enemies, enemyPosition, enemySpawnTime));

        if (isPickUpSpawn)
        {
            StartCoroutine(RandomObjectSpawner(pickUps, pickUpPosition, pickUpTime));          // spawn random objects
            StartCoroutine(RandomObjectSpawner(fuelSpawn, pickUpPosition, fuelSpawnTime));     // spawn every  time fuel
            StartCoroutine(RandomObjectSpawner(ammo, pickUpPosition, fuelSpawnTime));          // spawn every  time ammo
        }
    }


    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Level " + level + " loaded");
        if (level == 0)
        {
            Destroy(gameObject);
        }
        else if (level == 1)
        {
			Input.simulateMouseWithTouches = false;
            lifes = 3;
			score = 0;
            GetGUIReferences();
            SpawnPlayer();
        }
		else if (level == 2)
		{
			Destroy(gameObject);

		}
        
    }


    public void BulletTimeOn()
    {
        bulletTime.StartCoroutine("TurnOnBulletTime");
    }


    public void GameOver()
    {
        //lifesText.text = "";
        //scoreText.text = "";
		Debug.Log ("Something");
        StartCoroutine(ResetScene());
    }


    public void DescreaseLifes()
    {
        lifes--;
        //lifesText.text = "Lifes: " + lifes;
    }


    public void IncreaseScore(int amount)
    {
        this.score += amount;
        scoreText.text = "Score: " + score;

        if (score > nextGoldenScore)
        {
            scoreText.GetComponent<Animator>().SetTrigger(scoreAnimation);
            nextGoldenScore += goldenScore;                                  // set new  value when label will change his color to gold
        }
    }


    public void DropRandomItem(Vector3 spawnPoint)
    {
        int randomPercent = Random.Range(0, 100);                                         // random percent to drop item

        if (randomPercent > dropPercent) return;                                          // if randomPercent is greatest than dropPErcent, create item

        GameObject newDrop = Instantiate(dropItems[Random.Range(0, dropItems.Length)], spawnPoint, Quaternion.identity) as GameObject;
        newDrop.transform.SetParent(hierarchyGuard);
    }


    public void PlayerDeath()
    {
        Invoke("SpawnPlayer", 1);
    }
    

    void GetGUIReferences()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        //lifesText = GameObject.FindGameObjectWithTag("Life").GetComponent<Text>();
    }


    void SpawnPlayer()
    {
        Instantiate(playerHolder, new Vector3(0, 0, -4.0f), Quaternion.identity);
    }


    IEnumerator RandomObjectSpawner(GameObject[] objectsToSpawn, Vector3 objectsPosition, float objectSpawnTime)
    {
        while (true)                                                                                                                            // spawn objects all the time
        {
            yield return new WaitForSeconds(objectSpawnTime);
            GameObject randObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];                                                     // choose random object
            Vector3 randPosition = new Vector3(Random.Range(-objectsPosition.x, objectsPosition.x), objectsPosition.y, objectsPosition.z);      // choose random object position
            GameObject newObject = Instantiate(randObject, randPosition, Quaternion.identity) as GameObject;                                    // create new object
            newObject.transform.SetParent(hierarchyGuard);                                                                                      // parent Enemy to  hierarchyGuard
        }
    }


    IEnumerator RandomObjectSpawner(GameObject objectsToSpawn, Vector3 objectsPosition, float objectSpawnTime)                                  // method for only one gameobject
    {
        while (true)                                                                                                                            // spawn objects all the time
        {
            yield return new WaitForSeconds(objectSpawnTime);
            Vector3 randPosition = new Vector3(Random.Range(-objectsPosition.x, objectsPosition.x), objectsPosition.y, objectsPosition.z);      // choose random object position
            GameObject newObject = Instantiate(objectsToSpawn, randPosition, Quaternion.identity) as GameObject;                                // create new object
            newObject.transform.SetParent(hierarchyGuard);                                                                                      // parent Enemy to  hierarchyGuard
        }
    }


    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(4);
        Application.LoadLevel(2);
    }

}   // Karol Sobanski, Piotr Pusz
