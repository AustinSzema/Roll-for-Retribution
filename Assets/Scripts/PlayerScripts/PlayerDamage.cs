using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private intVariable _playerHealth;

    [SerializeField] private intVariable _playerCurrentHealth;

    [SerializeField] private GameObject _gameOverMenu;

    [SerializeField] private AudioClip _playerDamagedClip;

    [SerializeField] private intVariable score;

    [SerializeField] private TextMeshProUGUI displayHighScore;

    [SerializeField] private GameObject _mainCanvas;

    [SerializeField] private CameraShake _cameraShake;
    
    private HighScore _highScore;
    
    private static AudioManager _audioManager;

    private bool _gameOver;

    [SerializeField] private boolVariable _gameIsPaused;

    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
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
            //Time.timeScale = 0;
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
        StartCoroutine(_cameraShake.Shake(0.2f, 1f));
        _playerCurrentHealth.Value -= hitPoints;
        if (_playerCurrentHealth.Value <= 0)
        {
            Die();
        } else {
            _audioManager.PlayInvariableSFX(_playerDamagedClip);
            _gameManager.IncreaseSuperMeter();
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
       _highScore.WriteHighScore(score.Value + 1);
       _gameOverMenu.SetActive(true);

       InvokeRepeating("fadeOutMainCanvas", 0f, 0.1f);
       displayHighScore.text = "High Score: " + _highScore.GetHighScore();
       _gameIsPaused.Value = true;
    }
    
    
    //TODO: put this on maincanvas instead of on player damage 
    private void fadeOutMainCanvas()
    {
        RawImage mainCanvasImage = _mainCanvas.GetComponentInChildren<RawImage>();
        mainCanvasImage.color = new Color(mainCanvasImage.color.r, mainCanvasImage.color.g, mainCanvasImage.color.b,
            mainCanvasImage.color.a - 0.1f);
        if (mainCanvasImage.color.a <= 0f)
        {
            _mainCanvas.SetActive(false);
            CancelInvoke();
            Time.timeScale = 0f;
        }
    }
}
