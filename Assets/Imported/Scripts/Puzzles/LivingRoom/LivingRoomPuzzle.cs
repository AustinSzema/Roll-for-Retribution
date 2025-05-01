using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;
using UnityEngine.Events;

public class LivingRoomPuzzle : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private LetterAndOpener _letterAndOpener;
    [SerializeField] private ObjectFade _puzzleObjectLetter;
    [SerializeField] private MemoryRoom _livingRoom;
    [SerializeField] private MemoryRoomFadeSequence _livingRoomFadeSequence;
    [SerializeField] private VoiceoverSequence _playerCueSequence;
    [SerializeField] private AudioClip _openLetterFailedClip;
    [SerializeField] private AudioSource _playerAudioSource;

    [Header("Data")]
    [SerializeField] private BoolVariable[] _hasPieceFlagArray;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPuzzleFinished;

    private bool _canActivateLetter = false;

    private bool canPlayClip = true;

    private void Awake()
    {
        foreach (BoolVariable hasPieceFlag in _hasPieceFlagArray)
        {
            hasPieceFlag.Value = false;
        }
    }

    private void OnEnable()
    {
        foreach (BoolVariable hasPieceFlag in _hasPieceFlagArray)
        {
            hasPieceFlag.ValueUpdated += CheckPiecesAcquired;
        }
        _letterAndOpener.ValueUpdated += FinishPuzzle;
    }

    private void Start()
    {
        _letterAndOpener.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        foreach (BoolVariable hasPieceFlag in _hasPieceFlagArray)
        {
            hasPieceFlag.ValueUpdated -= CheckPiecesAcquired;
        }
        _letterAndOpener.ValueUpdated -= FinishPuzzle;
    }

    private void CheckPiecesAcquired()
    {
        foreach (BoolVariable hasPieceFlag in _hasPieceFlagArray)
        {
            if (hasPieceFlag.Value)
            {
                continue;
            }
            else
            {
                return;
            }
        }
        _playerCueSequence.Stop();
        _canActivateLetter = true;
    }

    private void FinishPuzzle()
    {
        _livingRoomFadeSequence.CancelInvoke();
        for (int i = 0; i <= _livingRoom.MaxPriority; i++)
        {
            _livingRoom.FadeInAllInPriority(i);
        }
        _onPuzzleFinished?.Invoke();
        Debug.Log("Puzzle Finished!");
    }

    public void OnActivateLetter()
    {
        if (_canActivateLetter)
        {
            _puzzleObjectLetter.Hide();
            _letterAndOpener.gameObject.SetActive(true);
        }
        else
        {
            if (canPlayClip)
            {
                _playerAudioSource.PlayOneShot(_openLetterFailedClip);
                canPlayClip = false;
                StartCoroutine(ClipCooldownRoutine());
            }
        }
    }

    private IEnumerator ClipCooldownRoutine()
    {
        yield return new WaitForSeconds(10);
        canPlayClip = true;
    }
}
