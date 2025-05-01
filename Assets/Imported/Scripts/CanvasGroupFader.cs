using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] private float _defaultFadeTime = 2.0f;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Fade(bool fadeIn)
    {
        StartCoroutine(FadeRoutine(fadeIn));
    }

    public void Fade(bool fadeIn, float fadeDuration)
    {
        StartCoroutine(FadeRoutine(fadeIn, fadeDuration));
    }

    public IEnumerator FadeRoutine(bool fadeIn)
    {
        yield return FadeRoutine(fadeIn, _defaultFadeTime);
    }

    public IEnumerator FadeRoutine(bool fadeIn, float fadeDuration)
    {
        float elapsedTime = fadeIn ?
            (_canvasGroup.alpha * fadeDuration) :
            (1 - _canvasGroup.alpha) * fadeDuration;
        float startAlpha = _canvasGroup.alpha;
        float endAlpha = fadeIn ? 1.0f : 0.0f;

        while (elapsedTime < fadeDuration)
        {
            _canvasGroup.alpha =
                Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = endAlpha;
    }
}
