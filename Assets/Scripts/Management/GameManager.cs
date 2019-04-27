using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Player Settings")]
    public ObjectPool playerPool;
    public Transform[] playerSpawnPoints;

    [Header("Zombie Settings")]
    public ObjectPool zombiePool;
    public Transform[] zombieSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        playerPool.GetObject();
        zombiePool.GetObject().gameObject.transform.position = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
