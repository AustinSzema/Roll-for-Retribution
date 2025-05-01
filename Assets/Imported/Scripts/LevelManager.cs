using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private Image crossHair;

    [SerializeField] private TextMeshProUGUI collectibleCountText;
    [SerializeField] public TextMeshProUGUI ammoCountText;
    

    [HideInInspector] public bool lookingAtEnemy = false;


    private Vector3 crosshairStartingSize;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    // Level Progress Tracker
    [SerializeField] private int currentLevelNumber = 1; 


    [SerializeField] private  Image fadeImage;
    [SerializeField] private GameObject blackout;
    [SerializeField] private GameObject finalWinScreen;

    private  float fadeDuration = 0.25f;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    private List<Collectible> collectibles;

    private void Start()
    {
        collectibles = FindObjectsByType<Collectible>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
        crosshairStartingSize = crossHair.transform.localScale;
        healthSlider.maxValue = PlayerHealth.instance.maxHealth;
        healthSlider.minValue = 0;
    }

    private void Update()
    {
        collectibleCountText.text = GetInactiveInList(collectibles) + "/" + collectibles.Count;
        if (lookingAtEnemy)
        {
            crossHair.transform.localScale = crosshairStartingSize / 2f;
            crossHair.color = Color.green;
        }
        else
        {
            crossHair.transform.localScale = crosshairStartingSize;
            crossHair.color = Color.white;
        }

        healthSlider.value = PlayerHealth.instance.currentHealth;
        healthText.text = "HP: " + PlayerHealth.instance.currentHealth + "/" + PlayerHealth.instance.maxHealth;
    }

    public int GetActiveInList(List<Collectible> list)
    {
        int count = 0;
        foreach (var col in list)
        {
            if (col.isActiveAndEnabled)
            {
                count++;
            }
        }

        return count;
    }

    public int GetInactiveInList(List<Collectible> list)
    {
        int count = 0;
        foreach (var col in list)
        {
            if (!col.isActiveAndEnabled)
            {
                count++;
            }
        }

        return count;
    }

    public bool AllCollectiblesCollected()
    {
        return GetInactiveInList(collectibles) >= collectibles.Count;
    }

    public bool gameIsPaused { get; private set; } = false;

    public void GameOver()
    {
        PauseGame();
        MusicPlayer.Instance.StopMusic();
        gameOverScreen.SetActive(true);
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        //Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    
    public void NextLevel()
    {
        if (AllCollectiblesCollected())
        {
            

            int savedLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            if (currentLevelNumber > savedLevel)
            {
                PlayerPrefs.SetInt("CurrentLevel", currentLevelNumber);
            }

        
            int keysCollectedThisLevel = GetInactiveInList(collectibles);
            int totalKeys = PlayerPrefs.GetInt("TotalKeysCollected", 0);
            totalKeys += keysCollectedThisLevel;
            PlayerPrefs.SetInt("TotalKeysCollected", totalKeys);

            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex >= 5)
            {


                finalWinScreen.gameObject.SetActive(true);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                gameIsPaused = true;
                return;

#if !UNITY_WEBGL
                Debug.Log("THE GAME IS OVER, PLEASE LEAVE");
                Application.Quit();
#endif
            }

            Time.timeScale = 1f;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                FadeToBlackAndLoadScene(nextSceneIndex);

            }
            else
            {
                // reload scene if no next scene

                FadeToBlackAndLoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void FirstLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    
    public void FadeToBlackAndLoadScene(int sceneIndex)
    {
        StartCoroutine(FadeAndLoad(sceneIndex));
    }

    private IEnumerator FadeAndLoad(int sceneIndex)
    {
        yield return StartCoroutine(Fade(0f, 1f)); // Fade to black

        yield return new WaitForSeconds(0.5f); // Optional pause

        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        blackout.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 1);
    }
}