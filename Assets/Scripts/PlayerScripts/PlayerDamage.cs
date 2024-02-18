using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour, IDamageable
{

    [SerializeField] private intVariable _playerHealth;

    [SerializeField] private intVariable _playerCurrentHealth;

    [SerializeField] private GameObject _gameOverMenu;
    private bool _gameOver;
    private void Start()
    {
        _playerCurrentHealth.Value = _playerHealth.Value;
        _gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        if (_gameOver)
        {
            Time.timeScale = 0;
        }
        
    }


    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            takeDamage(1);
        }
    }

    public void takeDamage(int hitPoints)
    {
        _playerCurrentHealth.Value -= hitPoints;
        if (_playerCurrentHealth.Value <= 0)
        {
            Die();
        }
        Debug.Log("player took 1 damage, currently at " + _playerCurrentHealth.Value + " health");
    }

    public void Die()
    {
       // freeze the game
       _gameOver = true;
       
       // menu popup
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
       _gameOverMenu.SetActive(true);
    }
}
