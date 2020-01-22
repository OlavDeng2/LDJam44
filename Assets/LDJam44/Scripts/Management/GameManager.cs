using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool spawnEnemies = false;

    [Header("Player Settings")]
    public ObjectPool playerPool;
    public Transform[] playerSpawnPoints;

    [Header("Zombie Settings")]
    public ObjectPool zombiePool;
    public SpawnPoint[] zombieSpawnPoints;
    public int zombiesToSpawn = 5;

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

        if(spawnEnemies)
        {
            SpawnEnemies(zombieSpawnPoints, zombiesToSpawn, zombiePool);
            
            //We are not spawning bandits anymore
            //SpawnEnemies(banditSpawnPoints, banditsToSpawn, banditPool);
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
