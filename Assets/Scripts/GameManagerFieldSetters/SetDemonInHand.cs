using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDemonInHand : MonoBehaviour
{

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        _gameManager.demonInHand = true;
    }

    private void OnTriggerStay(Collider other)
    {
        _gameManager.demonInHand = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _gameManager.demonInHand = false;
    }
}
