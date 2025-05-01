using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;

public class MemoryRoomFadeSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private MemoryRoom _memoryRoom;
    [SerializeField] private ApplicationManager _appManager;
    [SerializeField] private CanvasGroupFader _sceneFader;

    [Header("Fade Parameters")]
    [SerializeField] private float _levelTime = 60.0f;
    [SerializeField] private float _startDelay = 0.0f;
    private float _timeBetweenObjectFades;

    [Header("Data")]
    [SerializeField] private BoolVariable _isFirstLoop;
    [SerializeField] private BoolVariable _isFirstTimeFirstLevel;

    private int currentPriority;

    private void Start()
    {
        currentPriority = _memoryRoom.MinPriority;
        _timeBetweenObjectFades = _levelTime / _memoryRoom.GetFadeObjectCount();
    }

    public void StartFadeOutSequence()
    {
        InvokeRepeating("FadeOutObject", _startDelay, _timeBetweenObjectFades);
    }

    private void FadeOutObject()
    {
        bool priorityHasObjects =
            _memoryRoom.FadeOutRandomObjectInPriority(currentPriority, 1.0f);
        if (!priorityHasObjects)
        {
            if (currentPriority == _memoryRoom.MaxPriority)
            {
                CancelInvoke();
                
                if (_isFirstTimeFirstLevel.Value)
                {
                    return;
                }

                if (_isFirstLoop.Value)
                {
                    StartCoroutine(ReloadFirstSceneRoutine());
                }
                else
                {
                    StartCoroutine(LoadEndSceneRoutine());
                }

            }
            else
            {
                currentPriority++;
            }
        }
    }

    private IEnumerator ReloadFirstSceneRoutine()
    {
        yield return _sceneFader.FadeRoutine(true);
        _appManager.LoadSceneSingle(2);
    }

    private IEnumerator LoadEndSceneRoutine()
    {
        yield return _sceneFader.FadeRoutine(true);
        _appManager.LoadSceneSingle(5);
    }
}
