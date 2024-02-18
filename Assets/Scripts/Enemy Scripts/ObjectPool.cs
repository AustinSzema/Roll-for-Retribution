using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> _pooledObjects = new List<GameObject>();
    [SerializeField] private int _amountToPool;

    [SerializeField] private GameObject _enemyPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.GetComponentInChildren<Enemy>(true).gameObject.SetActive(false);
            _pooledObjects.Add(enemy);
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].GetComponentInChildren<Enemy>(true).gameObject.activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }


}