using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 21;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public static SkullObjectPool instance;


    [SerializeField] private Transform skullParent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, skullParent);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Instantiate(prefab);
        }
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public void ReturnAllObjects() 
    {
        for (int i = skullParent.childCount - 1; i >= 0; i--)
        {
            ReturnObject(skullParent.GetChild(i).gameObject);
        }
    }
}
