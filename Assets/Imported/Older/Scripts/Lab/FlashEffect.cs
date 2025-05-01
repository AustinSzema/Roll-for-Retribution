using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private float _effectFadeInTime = 1.0f;
    [SerializeField] private float _effectFadeOutTime = 1.0f;
    [SerializeField] private float _effectHoldTime = 0.25f;
    [SerializeField] private CanvasGroup _canvasGroup;

    private float _elapsedTime;

    public void PlayEffect()
    {
        StartCoroutine(FlashEffectFadeInRoutine());
    }

    private IEnumerator FlashEffectFadeInRoutine()
    {
        _elapsedTime = 0.0f;

        while (_elapsedTime < _effectFadeInTime)
        {
            _elapsedTime += Time.deltaTime;
            _canvasGroup.alpha =
                Mathf.Lerp(0.0f, 1.0f, _elapsedTime / _effectFadeInTime);
            yield return null;
        }

        _canvasGroup.alpha = 1.0f;
        StartCoroutine(FlashEffectHoldRoutine());
    }

    private IEnumerator FlashEffectHoldRoutine()
    {
        yield return new WaitForSeconds(_effectHoldTime);
        StartCoroutine(FlashEffectFadeOutRoutine());
    }

    private IEnumerator FlashEffectFadeOutRoutine()
    {
        _elapsedTime = 0.0f;

        while (_elapsedTime < _effectFadeOutTime)
        {
            _elapsedTime += Time.deltaTime;
            _canvasGroup.alpha =
                Mathf.Lerp(1.0f, 0.0f, _elapsedTime / _effectFadeOutTime);
            yield return null;
        }

        _canvasGroup.alpha = 0.0f;
    }
}
