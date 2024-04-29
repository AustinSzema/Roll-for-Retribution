using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetKillCountText : MonoBehaviour
{

    
    [SerializeField] private TextMeshProUGUI _killCountText;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        _killCountText.text = "Kill Count: " + _gameManager.killCount;
    }
}
