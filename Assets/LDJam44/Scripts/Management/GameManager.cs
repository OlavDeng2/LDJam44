using System.Collections;
using System.Collections.Generic;
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
    public float currentTime = 0f;
    public float spawnTime = 5f;
    public float timeSinceLastSpawn = 0f;

    [Header("Player Settings")]
    public ObjectPool playerPool;
    public Transform[] playerSpawnPoints;

    [Header("Zombie Settings")]
    public ObjectPool zombiePool;
    public SpawnPoint[] zombieSpawnPoints;
    //public int zombiesToSpawn = 5;

    [Header("Game Data")]
    public PooledObject player;
    public bool isPaused = false;
    public bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        Time.timeScale = 1;

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
        }

        //spwn enemies if you can spawn enemies and the round time isnt over yet
        if (spawnEnemies && currentTime <= roundTime * currentRound)
        {
            if(timeSinceLastSpawn > spawnTime)
            {
                SpawnEnemies(zombieSpawnPoints, initialZombies * currentRound, zombiePool);
                timeSinceLastSpawn = 0f;
            }

            timeSinceLastSpawn += Time.deltaTime;
            currentTime += Time.deltaTime;
        }

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

    private void SpawnEnemies(SpawnPoint[] allSpawnpoints, int amountToSpawn, ObjectPool objectPool)
    {
        foreach(SpawnPoint spawnPoint in allSpawnpoints)
        {
            for(int i = 0; i < spawnPoint.amountToSpawn; i++)
            {
                PooledObject enemy = objectPool.GetObject();
                enemy.GetComponent<Enemy>().gameManager = this;
                enemy.transform.position = spawnPoint.transform.position;
            }
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
