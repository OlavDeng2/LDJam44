using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab;
    public int spawnCount;

    public List<PooledObject> objects;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            GameObject go = Instantiate(objectPrefab, this.transform);
            PooledObject pooledObject = go.GetComponent<PooledObject>();
            pooledObject.pool = this;
            objects.Add(pooledObject);
            go.SetActive(false);
        }
    }

    public PooledObject GetObject()
    {
        PooledObject objectToGet = objects[objects.Count - 1];
        objects.RemoveAt(objects.Count - 1);
        objectToGet.gameObject.SetActive(true);
        return objectToGet;
    }

    public void ReturnToPool(PooledObject objectToReturn)
    {
        objects.Add(objectToReturn);
        objectToReturn.gameObject.SetActive(false);
    }
}
