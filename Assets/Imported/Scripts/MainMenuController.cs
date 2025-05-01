using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text playTimeText;
    public TMP_Text levelText;
    public TMP_Text keysText;
    public TMP_Text progressText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ⏱ Play Time
        float totalTime = PlayerPrefs.GetFloat("PlayTime", 0f);
        System.TimeSpan time = System.TimeSpan.FromSeconds(totalTime);
        playTimeText.text = "Play Time: " + time.ToString(@"hh\:mm\:ss");

        // 📊 Progress Tracking
        int level = PlayerPrefs.GetInt("CurrentLevel", 1);
        int keys = PlayerPrefs.GetInt("TotalKeysCollected", 0);

        int totalLevels = GameProgress.TotalLevels;
        int totalKeys = GameProgress.TotalKeysInGame;

        levelText.text = "Current Level: Level " + level;
        keysText.text = "Keys Found: " + keys + " / " + totalKeys;
        progressText.text = "Game Progress: " + level + " / " + totalLevels + " levels completed";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void OpenAbout()
    {
        SceneManager.LoadScene("AboutMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


