using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{    
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private int numEnemiesToSpawn = 5;

    [SerializeField] private int spawnInterval = 5;

    [SerializeField] private AudioClip _summonClip;

    private static AudioManager _audioManager;

    private SpawnEnemies _enemySpawner;

    [SerializeField] private GameObject enemyToSpawn;

    private bool _summoning;
    private bool _delaying;

    private GameManager _gameManager;

    private void Start()
    {
      _gameManager = GameManager.Instance;
    }

    private void Awake()
    {
      if(_summonClip == null)
      {
          _summonClip = Resources.Load<AudioClip>("Audio/SummonClip");
      }

      _audioManager = FindObjectOfType<AudioManager>();
      _enemySpawner = GameObject.FindObjectOfType<SpawnEnemies>();   
      _summoning = false;
      _delaying = true;
    }

    private void OnEnable()
    {
      StartCoroutine(InitialDelay());
    }

    private void FixedUpdate()
    {
      if (_gameManager.gameIsPaused == false && this.gameObject.activeSelf)
      {
        if (!_summoning && !_delaying)
        {
          StartCoroutine(Summon());
        }
        else
        {
          transform.position = Vector3.MoveTowards(transform.position,_gameManager.playerPosition, moveSpeed * Time.deltaTime);
        }
      }

    }

    IEnumerator Summon()
    {
      _summoning = true;
      for (int i = 0; i < numEnemiesToSpawn; i++)
      {
        if(i == 0 && _summonClip != null)
        {
            _audioManager.PlaySFXAtLocationWithVolume(_summonClip, transform.position, 800.0f);
        }
        _enemySpawner.SpawnEnemy(enemyToSpawn, transform.position, .5f, 1f);
      }
      yield return new WaitForSeconds(spawnInterval);
      _summoning = false;
    }

    IEnumerator InitialDelay()
    {
      _delaying = true;
      yield return new WaitForSeconds(5);
      _delaying = false;
    }
    
    
}
