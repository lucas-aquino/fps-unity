using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField]
    GameObject objectPrefab;

    [SerializeField]
    int poolSize = 5;

    
    Queue<GameObject> pool;


    private void Start()
    {
        this.InitPool(poolSize);
    }


    private void AddObject(GameObject obj)
    {
        this.pool.Enqueue(obj);
    }

    private void InitPool(int size)
    {
        if (pool != null)
            return;
        
        pool = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            this.AddObject(obj);
        }
    }

    private GameObject GetObject()
    {
        return this.pool.Dequeue();
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject obj = this.pool.Dequeue();

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        this.pool.Enqueue(obj);

        return obj;
    }
}
