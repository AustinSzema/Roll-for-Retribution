using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;

public class TheBeginningSequence : MonoBehaviour
{
    [SerializeField] private ApplicationManager _appManager;
    [SerializeField] private CanvasGroupFader _sceneFader;
    [SerializeField] private int _sceneToLoad;

    public void StartSequence()
    {
        StartCoroutine(FadeOutSequence());
    }

    public void StartInSequence()
    {
        StartCoroutine(FadeInSequence());
    }

    private IEnumerator FadeOutSequence()
    {
        _sceneFader.GetComponent<CanvasGroup>().blocksRaycasts = true;
        _sceneFader.GetComponent<CanvasGroup>().interactable = true;
        yield return _sceneFader.FadeRoutine(true);
        _appManager.LoadSceneSingle(_sceneToLoad);
    }

    private IEnumerator FadeInSequence()
    {
        _sceneFader.GetComponent<CanvasGroup>().blocksRaycasts = true;
        _sceneFader.GetComponent<CanvasGroup>().interactable = true;
        yield return _sceneFader.FadeRoutine(false);
        _sceneFader.GetComponent<CanvasGroup>().blocksRaycasts = false;
        _sceneFader.GetComponent<CanvasGroup>().interactable = false;
    }
}
