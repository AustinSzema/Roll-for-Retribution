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

    [SerializeField] private AudioClip _summonClip;

    private static AudioManager _audioManager;

    private SpawnEnemies _enemySpawner;

    [SerializeField] private GameObject enemyToSpawn;

    private bool _summoning;

    private void Awake()
    {
      if(_summonClip == null)
      {
          _summonClip = Resources.Load<AudioClip>("Audio/SummonClip");
      }

      _audioManager = FindObjectOfType<AudioManager>();
      _enemySpawner = GameObject.FindObjectOfType<SpawnEnemies>();   
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
      if (_summonClip != null)
      {
          _audioManager.PlaySFXAtLocation(_summonClip, transform.position);
      }
      else
      {
          Debug.LogWarning("Summon clip is null in " + gameObject.name);
      }
      for (int i = 0; i < numEnemiesToSpawn; i++)
      {
        _enemySpawner.SpawnEnemy(enemyToSpawn, this.transform.position);
      }
      yield return new WaitForSeconds(spawnInterval);
      _summoning = false;
    }
    
    
}
