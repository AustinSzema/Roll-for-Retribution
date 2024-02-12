using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [SerializeField] private List<EnemyAndNumber> enemiesToPool;

    // string is the .name field of the enemy gameobject in EnemyAndNumber in enemiesToPool
    private Dictionary<string, List<GameObject>> _enemyPool;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        for (int i = 0; i < enemiesToPool.Count; i++)
        {
            EnemyAndNumber current = enemiesToPool[i];
            for (int j = 0; j < current.number; j++)
            { 
                GameObject enemy = Instantiate(current.enemy);
                enemy.SetActive(false);
                if (_enemyPool.ContainsKey(current.enemy.name))
                {
                    _enemyPool[current.enemy.name].Add(enemy);
                }
                else
                {
                    List<GameObject> enemyList = new List<GameObject>();
                    _enemyPool.Add(current.enemy.name, enemyList);
                }
            }
       }

    }
    
    public GameObject GetPooledObject(string name)
    {
        // have to account for requesting an enemy type that isnt in here yet
        List<GameObject> currentEnemyPool;
        bool alreadyExists = _enemyPool.TryGetValue(name, out currentEnemyPool);
        if (alreadyExists)
        {
            for (int i = 0; i < currentEnemyPool.Count; i++)
            {
                if (currentEnemyPool[i].activeInHierarchy)
                {
                    return currentEnemyPool[i];
                }
            }
        }
        else
        {
            // this means the enemy type doesnt have a pool
            // TODO finish this
            return null;
        }


        return null;

    }
}
