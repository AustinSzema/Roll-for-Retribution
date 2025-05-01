using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToColor : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // Fullscreen UI image
    
    [SerializeField] private float fadeTime = 1.0f;
    private bool isFading = false;

    private void Start()
    {
        
        // Ensure it starts transparent
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
            fadeImage.gameObject.SetActive(false);
        }
    }

    // Simple Fade to Black
    public void FadeImage()
    {
        if (!isFading && fadeImage != null)
        {
            StartCoroutine(FadeOut());
        }
    }

    // Fade Out and Load Scene
    public void FadeImage(int index)
    {
        if (!isFading && fadeImage != null)
        {
            StartCoroutine(FadeAndLoadScene(index));
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        fadeImage.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeTime);
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }
        
        isFading = false;
    }

    private IEnumerator FadeAndLoadScene(int index)
    {
        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(0.2f); // Extra buffer if needed
        SceneManager.LoadScene(index);
    }
}