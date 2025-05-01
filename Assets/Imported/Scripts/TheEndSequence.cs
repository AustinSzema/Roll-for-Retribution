using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheEndSequence : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CanvasGroupFader _sceneFader;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private float _showMessageTime = 5.0f;
    [SerializeField] private float _timeBeforeEnd = 2.5f;

    public void StartEndSequence()
    {
        StartCoroutine(EndSequence());
    }
    public IEnumerator EndSequence()
    {
        yield return _sceneFader.FadeRoutine(false);
        yield return new WaitForSeconds(_showMessageTime);
        yield return _sceneFader.FadeRoutine(true);
        _text.text = "The End";
        _restartButton.SetActive(true);
        _sceneFader.GetComponent<CanvasGroup>().blocksRaycasts = true;
        yield return new WaitForSeconds(_timeBeforeEnd);
        yield return _sceneFader.FadeRoutine(false);
        _sceneFader.GetComponent<CanvasGroup>().blocksRaycasts = false;


    }
}
