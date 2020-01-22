using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool spawnEnemies = false;

    [Header("Game Settings")]
    public int currentRound = 5;
    public int initialZombies = 5;
    public float timeToNextRound = 60f; //time after killing last zombie til new round starts
    public float roundTime = 60f; // The amount of time in a round, in seconds.
    public float currentTime = 0f;

    [Header("Player Settings")]
    public ObjectPool playerPool;
    public Transform[] playerSpawnPoints;

    [Header("Zombie Settings")]
    public ObjectPool zombiePool;
    public SpawnPoint[] zombieSpawnPoints;
    //public int zombiesToSpawn = 5;

    //We no longer doing bandits
    /*
    [Header("Bandit Settings")]
    public ObjectPool banditPool;
    public SpawnPoint[] banditSpawnPoints;
    public int banditsToSpawn = 5;
    */

    [Header("Game Data")]
    public PooledObject player;
    public bool isPaused = false;
    public bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        Time.timeScale = 1;
    }

    private void Update()
    {
        /*

        //keep counting if on break
        if(!spawnEnemies &&currentTime <= timeToNextRound)
        {
            currentTime += Time.deltaTime;
        }
        //if break is over, reset time and start round
        else if(!spawnEnemies && currentTime >= timeToNextRound)
        {
            currentRound += 1;
            spawnEnemies = true;
            currentTime = 0f;
        }

        //spwn enemies if you can spawn enemies and the round time isnt over yet
        if (spawnEnemies && currentTime <= roundTime)
        {
            SpawnEnemies(zombieSpawnPoints, initialZombies * currentRound, zombiePool);

            currentTime += Time.deltaTime;
            //We are not spawning bandits anymore
            //SpawnEnemies(banditSpawnPoints, banditsToSpawn, banditPool);
        }

        else if (spawnEnemies && currentTime >= roundTime)
        {
            spawnEnemies = false;

            //reset the time
            currentTime = 0f;
        }

    */
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
