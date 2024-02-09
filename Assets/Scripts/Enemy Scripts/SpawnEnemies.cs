using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
  // the seconds between spawns
  [SerializeField] private int secondsBetweenSpawn;

  // Spawn Info is a scriptable object that contains a list of 
  // SpawnInfos, which are rules for spawning at different times
  [SerializeField] private spawnInfoVariable spawnInfo;

  [SerializeField] private Transform playerTransform;
  
  private List<SpawnInfo> _spawnInfos;

  private SpawnInfo _currentSpawnInfo;

  private int _currentSpawnInfoIdx;
  

  void Start()
  {
    if (spawnInfo == null)
    {
      throw new ArgumentNullException("spawnInfo must be non null");
    }

    _spawnInfos = spawnInfo.Value;
    
    if (_spawnInfos == null)
    {
      throw new ArgumentNullException("spawnInfo must contain a valid list of spawninfos");
    }

    _currentSpawnInfo = _spawnInfos[0];
    _currentSpawnInfoIdx = 0;

    InvokeRepeating("SpawnCurrentEnemies", 0f, secondsBetweenSpawn);
  }

  void Update()
  {
    if (_currentSpawnInfoIdx < _spawnInfos.Count - 1)
    {
      if (Time.timeSinceLevelLoad > _spawnInfos[_currentSpawnInfoIdx + 1].StartTime)
      {
        _currentSpawnInfoIdx += 1;
        _currentSpawnInfo = _spawnInfos[_currentSpawnInfoIdx];
      }
    }
  }

  void SpawnCurrentEnemies()
  {
    foreach (var spawnable in _currentSpawnInfo.Spawnables)
    {
      for (int i = 0; i < spawnable.number; i++)
      {
        
        GameObject enemyGameObject = Instantiate(spawnable.enemy);
        float maxDistanceFromPlayer = 25f;
        float minDistanceFromPlayer = 30f;

        float xOffset = Random.Range(maxDistanceFromPlayer, minDistanceFromPlayer) * Mathf.Sign(Random.Range(-1f, 1f));
        float zOffset = Random.Range(maxDistanceFromPlayer, minDistanceFromPlayer) * Mathf.Sign(Random.Range(-1f, 1f));

        enemyGameObject.transform.position = playerTransform.position + new Vector3(playerTransform.position.x + xOffset, 20f,
          playerTransform.position.z + zOffset);


        enemyGameObject.SetActive(true);
      }
    }
  }
}