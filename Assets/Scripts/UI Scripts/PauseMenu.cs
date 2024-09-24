using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioManager _audioManager;


    [SerializeField] private GameObject pauseMenuParent;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _audioManager = AudioManager.Instance;
    }

    public void Pause()
    {
        _gameManager.shopActive = !_gameManager.shopActive;
        if (_gameManager.shopActive)
        {
            pauseMenuParent.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            _gameManager.gameIsPaused = true;
        }
        else
        {
            pauseMenuParent.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            _gameManager.gameIsPaused = false;
        }

        _audioManager.PlayClickSound();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

}
