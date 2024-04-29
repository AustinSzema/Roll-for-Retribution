using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandPosition : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        _gameManager.handPosition = transform.position;
    }
}
