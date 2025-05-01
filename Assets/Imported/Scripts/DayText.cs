using TMPro;
using UnityEngine;

public class DayText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private float fadeDuration = 2f;

    private float fadeTimer;
    private Color originalColor;

    void Start()
    {
        dayText.text = "Day " + DayManagerSingleton.Instance.day;
        originalColor = dayText.color;
        fadeTimer = 0f;
    }

    void Update()
    {
        if (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float t = fadeTimer / fadeDuration;
            dayText.color = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, 0), t);
        }
    }
}