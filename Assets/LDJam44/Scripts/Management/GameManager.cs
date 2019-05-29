using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Global Pools")]
    public ObjectPool bulletPool;

    [Header("Player Settings")]
    public ObjectPool playerPool;
    public Transform[] playerSpawnPoints;

    [Header("Enemy Settings")]
    public ObjectPool zombiePool;
    public ObjectPool rangedZombiePool;
    public Transform[] enemySpawnPoints;

    [Header("Game Data")]
    public float currentScore = 0;
    public PooledObject player;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        Time.timeScale = 1;
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

    private void SpawnEnemies()
    {
        
        
    }

}
