using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
  // the seconds between spawns
  [SerializeField] private int secondsBetweenSpawn;

  [SerializeField] private RoundSpawnConfig _roundSpawnConfig;
  
  // Spawn Info is a scriptable object that contains a list of 
  // SpawnInfos, which are rules for spawning at different times
  private spawnInfoVariable currentSpawnInfoVariable;

  [SerializeField] private Transform playerTransform;

  [SerializeField] private float noNoZoneDistance = 100f;

  [SerializeField] private float noNoZoneAngle = 45f;

  public static int EnemiesInScene = 0;
  
  private List<SpawnInfo> _spawnInfos;

  private SpawnInfo _currentSpawnInfo;
  
  private int _currentSpawnInfoIdx;

  private bool _changeSpawnInfo;

  private List<spawnInfoVariable> _roundSpawns;

  
  void Start()
  {
    currentSpawnInfoVariable = _roundSpawnConfig.Value[GameManager.Instance.currentRound];
    if (_roundSpawnConfig == null)
    {
      throw new ArgumentNullException("_roundSpawnConfig must be non null");
    }

    _roundSpawns = _roundSpawnConfig.Value;
    
    if (_roundSpawns == null)
    {
      throw new ArgumentNullException("roundSpawns must contain a valid list of spawnInfoVariables");
    }
    
    
    if (currentSpawnInfoVariable == null)
    {
      throw new ArgumentNullException("spawnInfo must be non null");
    }

    _spawnInfos = currentSpawnInfoVariable.Value;
    
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
      //Debug.Log("Current spawn info is index " + _currentSpawnInfoIdx);
      StartCoroutine(SwapSpawnInfo());
    }

    if (_currentSpawnInfoIdx >= _spawnInfos.Count-1) // if we are on the last spawn info and there are no enemies in the scene
    {
      if (_currentSpawnInfo.StartTime <= Time.timeSinceLevelLoad) // mfw I nest an if statement
      {
        GameManager.Instance.currentRound++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }
    //Debug.Log(EnemiesInScene + " enemies in scene");
    Debug.Log("Current Spawn Info Index" + _currentSpawnInfoIdx + "   Count of spawn infos" + _spawnInfos.Count);
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
        yield return new WaitForSeconds(timeUntilSwap - .1f);
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
      //Debug.Log("I am about to spawn " + spawnable.number + " enemies");
      for (int i = 0; i < spawnable.number; i++)
      {
        if (EnemiesInScene >= _currentSpawnInfo.MaxEnemiesInScene && _currentSpawnInfo.MaxEnemiesInScene != -1)
        {
          break;
        }

        SpawnEnemy(_currentSpawnInfo.Spawnables[j].enemy, playerTransform.position);
        
      }
    }
  }

  bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
  {
    Vector3 cp1 = Vector3.Cross(b - a, p1 - a);
    Vector3 cp2 = Vector3.Cross(b - a, p2 - a);

    return Vector3.Dot(cp1, cp2) >= 0;
  }

  bool InTriangle(Vector3 point, Vector3 a, Vector3 b, Vector3 c)
  {
    return SameSide(point,a, b,c) && SameSide(point,b, a,c) && SameSide(point, c, a, b);
  }

  Vector3 PickEnemyPosition(Vector3 centralLocation, float minDistance = 40f, float maxDistance = 60f)
  {
    float xOffset = Random.Range(minDistance, maxDistance) * Mathf.Sign(Random.Range(-1f, 1f));
    float zOffset = Random.Range(minDistance, maxDistance) * Mathf.Sign(Random.Range(-1f, 1f));
    Vector3 enemyPosition = new Vector3(centralLocation.x + xOffset, 15.0f,centralLocation.z + zOffset);
    return enemyPosition;
  }

  public void SpawnEnemy(GameObject enemy, Vector3 centralLocation, float minRange = 40f, float maxRange = 60f)
  {
    GameObject enemyInstance = EnemyPool.Instance.GetPooledObject(enemy.name);

    Vector3 behind = -playerTransform.forward;
    Vector3 NoNoZoneLeft = Quaternion.AngleAxis(noNoZoneAngle, playerTransform.up) * behind * noNoZoneDistance;
    Vector3 NoNoZoneRight = Quaternion.AngleAxis(-noNoZoneAngle, playerTransform.up) * behind * noNoZoneDistance;

    Vector3 enemyPosition = PickEnemyPosition(centralLocation, minRange, maxRange);
    Debug.DrawLine(centralLocation, NoNoZoneLeft + centralLocation, Color.red, 5);
    Debug.DrawLine(centralLocation, NoNoZoneRight + centralLocation, Color.red, 5);
    while (InTriangle(enemyPosition, centralLocation, NoNoZoneLeft + centralLocation, NoNoZoneRight + centralLocation))
    {
      enemyPosition = PickEnemyPosition(centralLocation, minRange, maxRange);
    }

    enemyInstance.transform.position = enemyPosition;
    enemyInstance.GetComponentInChildren<Enemy>(true).gameObject.SetActive(true); // TODO: we can probably optimize this
    EnemiesInScene++;
  }
   
}