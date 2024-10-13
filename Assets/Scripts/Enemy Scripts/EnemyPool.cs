using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [SerializeField] private List<EnemyAndNumber> enemiesToPool;

    // string is the .name field of the enemy gameobject in EnemyAndNumber in enemiesToPool
    private Dictionary<string, List<GameObject>> _enemyPool;

    private Dictionary<string, EnemyAndNumber> _lookupTable;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Dictionary<string, List<GameObject>> dict = new Dictionary<string, List<GameObject>>();
        _enemyPool = dict;
        Dictionary<string, EnemyAndNumber> lookupDict = new Dictionary<string, EnemyAndNumber>();
        _lookupTable = lookupDict;
        for (int i = 0; i < enemiesToPool.Count; i++)
        {
            EnemyAndNumber current = enemiesToPool[i];
            for (int j = 0; j < current.number; j++)
            { 
                //Debug.Log(current.enemy + " ENEMY NAME RAHH");

                GameObject enemy = Instantiate(current.enemy);
                //Debug.Log(enemy.name + " ENEMY NAME RAHH");
                enemy.GetComponentInChildren<EnemyBase>(true).gameObject.SetActive(false);
                if (_enemyPool.ContainsKey(current.enemy.name))
                {
                    _enemyPool[current.enemy.name].Add(enemy);
                }
                else
                {
                    List<GameObject> enemyList = new List<GameObject>();
                    _enemyPool.Add(current.enemy.name, enemyList);
                    _lookupTable.Add(current.enemy.name, current);
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
            for (int i = 0; i < (_enemyPool[name]).Count; i++)
            {
                if (!(_enemyPool[name])[i].GetComponentInChildren<EnemyBase>(true).gameObject.activeInHierarchy)
                {
                    return (_enemyPool[name])[i];
                }
            }
            
            
                // we couldnt find any inactive members in the scene
                // so we should make another
                EnemyAndNumber inst = new EnemyAndNumber();
                if (_lookupTable.TryGetValue(name, out inst))
                {
                    GameObject addToPool = Instantiate(inst.enemy); 
                    addToPool.GetComponentInChildren<EnemyBase>(true).gameObject.SetActive(false);
                    (_enemyPool[name]).Add(addToPool);
                    return addToPool;
                }
            

        }
        else
        {
            // this means the enemy type doesnt have a pool
            // we can either make a pool for it or just return null, for now return null
            return null;
        }


        return null;

    }
}
