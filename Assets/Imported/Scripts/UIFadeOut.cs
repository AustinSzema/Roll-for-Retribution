using UnityEngine;
using UnityEngine.UI;

public class UIFadeOut : MonoBehaviour
{
    public float fadeDuration = 2f; // Duration of the fade
    private Image image;
    private Color startColor = Color.black;
    private Color endColor = new Color(0, 0, 0, 0); // Transparent black

    void Awake()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            image.color = startColor;
        }
    }

    void Start()
    {
        if (image != null)
        {
            StartCoroutine(FadeOut());
        }
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            image.color = Color.Lerp(startColor, endColor, t);
            timer += Time.deltaTime;
            yield return null;
        }

        image.color = endColor;
        gameObject.SetActive(false);
    }
}