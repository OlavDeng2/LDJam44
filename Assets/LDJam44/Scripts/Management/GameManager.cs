using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool spawnEnemies = false;

    [Header("Game Settings")]
    public bool inbetweenRounds = true;
    public int currentRound = 0;
    public int initialZombies = 1; // This is per spawnpoint per time it is allowed to spawn in a round, multiplied by round count
    public float timeToNextRound = 10f; //time between rounds, will be multiplied by round count
    public float roundTime = 60f; // The amount of time in a round, in seconds. Will be multiplied by round count
    public int enemyWaves = 6;
    public float currentTime = 0f;
    public float spawnTime = 5f;
    public float timeSinceLastSpawn = 0f;

    [Header("Player Settings")]
    public ObjectPool playerPool;
    public Transform[] playerSpawnPoints;

    [Header("Shop Settings")]
    public GameObject shop;

    [Header("Zombie Settings")]
    public ObjectPool zombiePool;
    public SpawnPoint[] zombieSpawnPoints;
    public int[] zombiesPerRound;
    private float timePerWave = 0f;
    private int zombiesPerWave = 0;

    [Header("Game Data")]
    public PooledObject player;
    public bool isPaused = false;
    public bool gameOver = false;


    // Start is called before the first frame update
    void OnEnable()
    {
        SpawnPlayer();
        Time.timeScale = 1;

        timePerWave = roundTime / enemyWaves;

        //SpawnEnemies(zombieSpawnPoints, initialZombies * currentRound, zombiePool);

    }

    private void Update()
    {
        //keep counting if on break
        if(inbetweenRounds && currentTime < timeToNextRound * currentRound)
        {
            currentTime += Time.deltaTime;
        }

        //if break is over, reset time and start round
        else if(inbetweenRounds && currentTime >= timeToNextRound * currentRound)
        {
            currentRound += 1;
            spawnEnemies = true;
            inbetweenRounds = false;
            currentTime = 0f;
            Shop shopScript = shop.GetComponent<Shop>();

            switch (currentRound)
            {
                case 1:
                    shopScript.UpdateLootTable(shopScript.lootTable1);
                    break;
                case 5:
                    shopScript.UpdateLootTable(shopScript.lootTable2);
                    break;
                case 10:
                    shopScript.UpdateLootTable(shopScript.lootTable3);
                    break;
                case 15:
                    shopScript.UpdateLootTable(shopScript.lootTable4);
                    break;
                default:
                    break;
            }

            shopScript.RefreshStore();

            
            //Only change the zombies to spawn per wave if there is a new number to change to
            if (zombiesPerRound.Length > currentRound)
            {
                zombiesPerWave = zombiesPerRound[currentRound] / enemyWaves;
            }
            else
            {
                return;
            }

            StartCoroutine(ZombieSpawn(timePerWave, zombiesPerWave));

        }

        //spwn enemies if you can spawn enemies and the round time isnt over yet
        if (spawnEnemies && currentTime <= roundTime * currentRound)
        {
            
            timeSinceLastSpawn += Time.deltaTime;
            currentTime += Time.deltaTime;
        }

        //End the round

        else if (spawnEnemies && currentTime >= roundTime * currentRound)
        {
            spawnEnemies = false;
            inbetweenRounds = true;

            //reset the time
            currentTime = 0f;
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnPlayer()
    {
        if(player)
        {
            player.ReturnToPool();
        }
        //select a random spawnpoint to spawn player
        Transform randomSpawnPos = playerSpawnPoints[UnityEngine.Random.Range(0, playerSpawnPoints.Length - 1)];
        player = playerPool.GetObject();
        Player playerScript = player.GetComponent<Player>();
        playerScript.health = playerScript.defaultHealth;
        playerScript.gameManager = this;
        
        player.transform.position = randomSpawnPos.position;
    }

    IEnumerator ZombieSpawn(float time, int zombiesToSpawn)
    {
        while (spawnEnemies)
        {
            for (int i = 0; i <= zombiesToSpawn; i++)
            {

                Debug.Log("Spawned zombie");
                SpawnPoint spawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length - 1)];
                PooledObject enemy = zombiePool.GetObject();
                enemy.GetComponent<Enemy>().gameManager = this;
                enemy.GetComponent<Enemy>().alive = true;
                enemy.GetComponent<Enemy>().health = enemy.GetComponent<Enemy>().defaultHealth;
                enemy.transform.position = spawnPoint.transform.position;

            }

            yield return new WaitForSeconds(12);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
}
