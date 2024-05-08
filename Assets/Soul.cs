using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    // TODO: Make this managed by a manager (data oriented) instead of individual scripts (object oriented) 

    private GameManager _gameManager;

//    public bool followPlayer = false;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        //if (followPlayer)
        //{

        //}
        transform.position = Vector3.MoveTowards(transform.position, _gameManager.playerPosition, Time.deltaTime * 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // TODO: Strings are dumb
        {
            CollectSoul();
        }
    }

    private void CollectSoul()
    {
        _gameManager.soulCount++;
        Debug.Log("collected soul");
        gameObject.SetActive(false);        
    }
}
