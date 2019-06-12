using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool spawnEnemies = false;

    [Header("Global Pools")]
    public ObjectPool bulletPool;

    [Header("Player Settings")]
    public ObjectPool playerPool;
    public Transform[] playerSpawnPoints;

    [Header("Zombie Settings")]
    public ObjectPool zombiePool;
    public Transform[] zombieSpawnPoints;
    public int zombiesToSpawn = 5;

    [Header("Bandit Settings")]
    public ObjectPool banditPool;
    public Transform[] banditSpawnPoints;
    public int banditsToSpawn = 5;

    [Header("Game Data")]
    public PooledObject player;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        Time.timeScale = 1;

        if(spawnEnemies)
        {
            SpawnEnemies(zombieSpawnPoints, zombiesToSpawn, zombiePool);
            SpawnEnemies(banditSpawnPoints, banditsToSpawn, banditPool);
        }
  

    }

    // Update is called once per frame
    void Update()
    {
        //If player is dead, game over
        if(!player.GetComponent<Character>().alive)
        {
            player.GetComponent<Player>().playerUI.GameOver();
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
        Player playerScript = player.GetComponent<Player>();
        playerScript.bulletPool = bulletPool;
        playerScript.health = playerScript.defaultHealth;
        
        player.transform.position = randomSpawnPos.position;
    }

    private void SpawnEnemies(Transform[] allSpawnpoints, int amountToSpawn, ObjectPool objectPool)
    {
        foreach(Transform spawnPoint in allSpawnpoints)
        {
            for(int i = 0; i <= amountToSpawn; i++)
            {
                PooledObject enemy = objectPool.GetObject();
                enemy.transform.position = spawnPoint.position;
            }
        }
    }

}
