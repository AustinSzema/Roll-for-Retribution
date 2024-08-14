using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour, IDamageable
{

    [SerializeField] private GameObject _gameOverMenu;

    [SerializeField] private AudioClip _playerDamagedClip;
    
    [SerializeField] private TextMeshProUGUI displayHighScore;

    [SerializeField] private GameObject _mainCanvas;

    [SerializeField] private CameraShake _cameraShake;
    
    private HighScore _highScore;
    
    private static AudioManager _audioManager;
    
    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;

        _gameManager.playerCurrentHealth = _gameManager.playerMaxHealth;
        
        _gameOverMenu.SetActive(false);
        _highScore = new HighScore();
        _audioManager = AudioManager.Instance;
        
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
        StartCoroutine(_cameraShake.Shake(0.2f, 1f));
        _gameManager.playerCurrentHealth -= hitPoints;
        if (_gameManager.playerCurrentHealth <= 0)
        {
            Die();
        } else {
            _audioManager.PlayInvariableSFX(_playerDamagedClip);
            _gameManager.IncreaseSuperMeter();
        }
        EZDebug.Log("player took 1 damage, currently at " + _gameManager.playerCurrentHealth + " health");
        
    }

    public void Die()
    {
       // menu popup
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
       int currentScore = _gameManager.killCount + 1;
       _highScore.WriteHighScore(currentScore);
       _gameOverMenu.SetActive(true);

       InvokeRepeating("fadeOutMainCanvas", 0f, 0.1f);
       displayHighScore.text = "High Score: " + _highScore.GetHighScore() + "\n Current Score: " + currentScore;
       _gameManager.gameIsPaused = true;
    }
    
    
    //TODO: put this on maincanvas instead of on player damage 
    private void fadeOutMainCanvas()
    {
        _mainCanvas.SetActive(false);
        CancelInvoke();
        _gameManager.enemiesShouldMove = false;

       //  RawImage mainCanvasImage = _mainCanvas.GetComponentInChildren<RawImage>();
       //  mainCanvasImage.color = new Color(mainCanvasImage.color.r, mainCanvasImage.color.g, mainCanvasImage.color.b, mainCanvasImage.color.a - 0.1f);
       //  if (mainCanvasImage.color.a <= 0f)
       //  {
       //      _mainCanvas.SetActive(false);
       //      CancelInvoke();
       //      _gameManager.enemiesShouldMove = false;
       // }
    }
}
