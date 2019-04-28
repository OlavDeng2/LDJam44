using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float waveLength = 60; //this is in seconds
    public float timeBetweenEnemySpawns = 5; // how long to wait before spawning a batch of zombies
    public int amountOfEnemiesToSpawn = 5;

    [Header("Player Settings")]
    public ObjectPool playerPool;
    public Transform[] playerSpawnPoints;

    [Header("Enemy Settings")]
    public ObjectPool zombiePool;
    public ObjectPool rangedZombiePool;
    public Transform[] enemySpawnPoints;

    [Header("Shop Settings")]
    public ObjectPool shopPool;
    public Transform[] shopSpawnPoints;

    [Header("UI Manager")]
    UIManager uiManager;

    [Header("Game Data")]
    public float timeSinceLastWave = 0;
    public float timeSinceLastSpawn = 0;
    public float currentScore = 0;
    public float currentWave = 0;
    public bool inBetweenWaves = false;
    public List<PooledObject> allEnemies;
    public PooledObject player;
    public PooledObject shop;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        SpawnPlayer();
        inBetweenWaves = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inBetweenWaves)
        {
            //Spawn enemies
            timeSinceLastSpawn += Time.deltaTime;
            timeSinceLastWave += Time.deltaTime;
            if (timeSinceLastSpawn > timeBetweenEnemySpawns)
            {
                timeSinceLastSpawn = 0;
                SpawnEnemies();
            }

            //if certain amount of time has passed
            if(timeSinceLastWave >= waveLength)
            {
                EndWave();
            }
        }

        if(inBetweenWaves)
        {
            //if enter is pressed, start wave
            if(Input.GetKeyDown(KeyCode.Return))
            {
                StartWave();
            }
        }

        //If player is dead, game over
        if(!player.GetComponent<Character>().alive)
        {
            uiManager.GameOver();
        }
    }

    private void SpawnPlayer()
    {
        if(player)
        {
            player.ReturnToPool();
        }
        //select a random spawnpoint to spawn player
        Transform randomSpawnPos = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Length - 1)];
        player = playerPool.GetObject();
        player.transform.position = randomSpawnPos.position;
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            //select a random number for chosing what to spawn
            int randomEnemyType = Random.Range(0, 1);
            //select a random spawnpoint to spawn enemy
            Transform randomSpawnPos = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length - 1 )];

            //Based on the previous numbers spawn an enemy
            switch (randomEnemyType)
            {
                case 0:
                    PooledObject enemy = zombiePool.GetObject();
                    allEnemies.Add(enemy);
                    enemy.gameObject.transform.position = randomSpawnPos.position;
                    break;
            }
        }
    }

    private void EndWave()
    {
        //Clear the previous wave
        foreach (PooledObject enemy in allEnemies)
        {
            enemy.ReturnToPool();

        }

        allEnemies.Clear();
        timeSinceLastWave = 0;

        //Spawn the shop
        if (shop)
        {
            shop.ReturnToPool();
        }
        inBetweenWaves = true;
        Transform randomSpawnPos = shopSpawnPoints[Random.Range(0, shopSpawnPoints.Length - 1)];
        shop = shopPool.GetObject();
        shop.transform.position = randomSpawnPos.position;
    }

    private void StartWave()
    {
        shop.ReturnToPool();
        inBetweenWaves = false;
    }
}
