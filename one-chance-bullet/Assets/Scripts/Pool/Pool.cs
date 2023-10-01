using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pool : MonoBehaviour
{
    float garbageCollectorTime = 60;
    List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        if(garbageCollectorTime > 0)
            StartCoroutine(GarbageCollectorCO());
    }

    IEnumerator GarbageCollectorCO()
    {
        while(garbageCollectorTime > 0)
        {
            yield return new WaitForSeconds(garbageCollectorTime);

            if (pool == null)
                continue;

            for(int i = 0; i < pool.Count; i++)
            {
                if (pool[i].activeInHierarchy == false)
                {
                    Destroy(pool[i].gameObject);
                    pool.Remove(pool[i]);
                    i--;
                }
            }

            if(pool.Count == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public static GameObject CreateObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        Pool pool = GetPool(obj.name);

        if (pool.pool == null)
            pool.pool = new List<GameObject>();

        foreach (var item in pool.pool)
        {
            if (item.activeSelf == false && item.name.Contains(obj.name))
            {
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.SetActive(true);
                return item;
            }
        }

        GameObject newInstance = Instantiate(obj, position, rotation);
        newInstance.name = obj.name;
        newInstance.transform.SetParent(pool.transform);
        pool.pool.Add(newInstance.gameObject);

        var poolConfig = newInstance.GetComponent<PoolConfiguration>();
        if(poolConfig != null)
        {
            pool.garbageCollectorTime = poolConfig.garbageCollectorTime;
        }

        return newInstance;
    }

    public static Pool GetPool(string objectName)
    {
        Pool[] pools = FindObjectsOfType<Pool>();
        for(int i = 0; i < pools.Length; i++)
        {
            if (pools[i].name.Contains(objectName + "_Pool"))
                return pools[i];
        }

        var poolObject = new GameObject(objectName + "_Pool");
        return poolObject.AddComponent<Pool>();
    }
}
