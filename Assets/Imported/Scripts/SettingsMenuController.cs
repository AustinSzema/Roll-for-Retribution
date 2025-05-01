using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TMP_Text sensitivityValueText;

    void Start()
    {
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2f);

        // Set slider and label to saved value
        sensitivitySlider.value = savedSensitivity;
        UpdateLabel(savedSensitivity);

        // Connect slider to update function
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    void OnSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        UpdateLabel(value);
    }

    void UpdateLabel(float value)
    {
        sensitivityValueText.text = "Mouse Sensitivity: " + value.ToString("F1");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

