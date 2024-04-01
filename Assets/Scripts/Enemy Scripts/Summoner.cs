using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{    
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private int numEnemiesToSpawn = 5;

    [SerializeField] private int spawnInterval = 5;
    
    [SerializeField] private Vector3Variable playerPosition;

    [SerializeField] private boolVariable gameIsPaused;

    [SerializeField] private SpawnEnemies enemySpawner;

    [SerializeField] private GameObject enemyToSpawn;

    private bool _summoning;

    private void Awake()
    {
      _summoning = false;
    }


    private void FixedUpdate()
    {
      if (gameIsPaused.Value == false && this.gameObject.activeSelf)
      {
        if (_summoning == false)
        {
          StartCoroutine(Summon());
        }
        else
        {
          transform.position = Vector3.MoveTowards(transform.position,playerPosition.Value, moveSpeed * Time.deltaTime);
        }
      }

    }

    IEnumerator Summon()
    {
      _summoning = true;
      for (int i = 0; i < numEnemiesToSpawn; i++)
      {
        enemySpawner.SpawnEnemy(enemyToSpawn);
      }
      yield return new WaitForSeconds(spawnInterval);
      _summoning = false;
    }
    
    
}
