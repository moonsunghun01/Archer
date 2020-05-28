using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;
    public GameObject bulletPrefab;
    public int maxPool = 50;
    public List<GameObject> bulletPool = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        CreatePooling();
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].activeSelf == false)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");

        for (int i = 0; i <maxPool; i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, objectPools.transform);
            obj.name = "Arrow_" + i.ToString("00");

            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }
}
