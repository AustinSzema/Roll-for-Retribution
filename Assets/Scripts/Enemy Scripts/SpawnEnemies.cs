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

  private bool _changeSpawnInfo;
  

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
    _changeSpawnInfo = true;

    InvokeRepeating("SpawnCurrentEnemies", 0f, secondsBetweenSpawn);
  }

  void Update()
  {
    if (_changeSpawnInfo)
    {
      Debug.Log("Current spawn info is index " + _currentSpawnInfoIdx);
      StartCoroutine(SwapSpawnInfo());
    }
  }

  IEnumerator SwapSpawnInfo()
  {
    // If we have reached the end of the list of spawn infos
    // then we have no new spawn info to swap to, so we stay 
    // on the last one
    if (_currentSpawnInfoIdx < _spawnInfos.Count - 1)
    {
      float timeUntilSwap = _spawnInfos[_currentSpawnInfoIdx + 1].StartTime - Time.timeSinceLevelLoad;
      // If the time to start the next spawn info is upon us
      // set the current spawn info to the next one
      if (timeUntilSwap < 0)
      {
        _currentSpawnInfoIdx += 1;
        _currentSpawnInfo = _spawnInfos[_currentSpawnInfoIdx];
      }
      else
      {
        // If it isn't time to swap yet, wait until it is time to swap
        // before checking again
        _changeSpawnInfo = false;
        yield return new WaitForSeconds(timeUntilSwap);
        _changeSpawnInfo = true;
      }
    }
    else
    {
      // if weve reached the end of the spawn info list
      // then we will never swap spawn infos again
      // so we just set it to false permaenently
      _changeSpawnInfo = false;
    }
  }

  void SpawnCurrentEnemies()
  {
    for (int j = 0; j < _currentSpawnInfo.Spawnables.Count; j++)
    {
      EnemyAndNumber spawnable = _currentSpawnInfo.Spawnables[j];
      Debug.Log("I am about to spawn " + spawnable.number + " enemies");
      for (int i = 0; i < spawnable.number; i++)
      {
        
        //GameObject enemyGameObject = Instantiate(spawnable.enemy);
        GameObject enemyGameObject = EnemyPool.Instance.GetPooledObject(_currentSpawnInfo.Spawnables[j].enemy.name);
        float maxDistanceFromPlayer = 25f;
        float minDistanceFromPlayer = 30f;

        float xOffset = Random.Range(maxDistanceFromPlayer, minDistanceFromPlayer) * Mathf.Sign(Random.Range(-1f, 1f));
        float zOffset = Random.Range(maxDistanceFromPlayer, minDistanceFromPlayer) * Mathf.Sign(Random.Range(-1f, 1f));

        enemyGameObject.transform.position = playerTransform.position + new Vector3(playerTransform.position.x + xOffset, 20f,
          playerTransform.position.z + zOffset);


        enemyGameObject.GetComponentInChildren<Enemy>(true).gameObject.SetActive(true);
      }
    }
  }
}