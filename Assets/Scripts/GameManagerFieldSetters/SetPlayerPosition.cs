using System;
using UnityEngine;

public class SetPlayerPosition : MonoBehaviour
{

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        _gameManager.playerPosition = transform.position;
        
    }
}
