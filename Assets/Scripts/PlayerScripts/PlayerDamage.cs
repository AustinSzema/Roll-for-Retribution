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

    [SerializeField] private AudioClip _playerDamagedClip;

    [SerializeField] private intVariable score;

    private HighScore _highScore;
    
    private static AudioManager _audioManager;

    private bool _gameOver;
    private void Start()
    {
        _playerCurrentHealth.Value = _playerHealth.Value;
        _gameOverMenu.SetActive(false);
        _highScore = new HighScore();
        _audioManager = FindObjectOfType<AudioManager>();

        if (_playerDamagedClip == null)
        {
            _playerDamagedClip = Resources.Load<AudioClip>("Audio/PlayerHit");
        }
    }

    private void Awake()
    {
        Time.timeScale = 1;
        _gameOver = false;
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
        Enemy enemyScript = other.gameObject.GetComponentInParent<Enemy>();
        if (enemyScript != null)
        {
            // if enemy touches player, take one damage
            takeDamage(1);
            
            // If an enemy touches the player, the enemy should die. Int32 MaxValue might be overkill but it should ensure that nothing lives
            enemyScript.takeDamage(Int32.MaxValue);
        }
    }

    public void takeDamage(int hitPoints)
    {
        _playerCurrentHealth.Value -= hitPoints;
        if (_playerCurrentHealth.Value <= 0)
        {
            Die();
        } else {
            _audioManager.PlayInvariableSFX(_playerDamagedClip);
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
       _highScore.WriteHighScore(score.Value);
       _gameOverMenu.SetActive(true);
    }
}
