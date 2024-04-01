using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{    
    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private Vector3Variable _playerPosition;

    [SerializeField] private boolVariable _gameIsPaused;

    [SerializeField] private SpawnEnemies enemySpawner;

    private bool _summoning;

    private void Awake()
    {
      _summoning = false;
    }


    private void FixedUpdate()
    {
      if (_gameIsPaused.Value == false && this.gameObject.activeSelf)
      {
        if (_summoning == false)
        {
          StartCoroutine(Summon());
        }
        else
        {
          transform.position = Vector3.MoveTowards(transform.position,_playerPosition.Value, _moveSpeed * Time.deltaTime);
        }
      }

    }

    IEnumerator Summon()
    {
      _summoning = true;
      yield return new WaitForSeconds(3);
      _summoning = false;
    }
    
    
}
