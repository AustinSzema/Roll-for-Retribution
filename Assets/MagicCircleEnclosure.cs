using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleEnclosure : MonoBehaviour
{
    private GameManager _gameManager;



    [Tooltip("The threshold at which the enclosure should deactivate")] [SerializeField] private int deactivationThreshold = 5000;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.killCount >= deactivationThreshold)
        {
            gameObject.SetActive(false);
        }
    }
}
