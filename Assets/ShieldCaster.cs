using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCaster : MonoBehaviour
{
    [SerializeField] private GameObject _shieldPrefab;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CastShield();
        }
        
    }

    private void CastShield()
    {
        GameObject shield = Instantiate(_shieldPrefab, _gameManager.playerPosition + Camera.main.transform.forward * 10f,  Camera.main.transform.rotation);
     }
}
