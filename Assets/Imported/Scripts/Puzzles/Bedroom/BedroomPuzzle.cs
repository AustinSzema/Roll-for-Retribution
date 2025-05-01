using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;
using UnityEngine.Events;

public class BedroomPuzzle : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _handDrumObject;
    [SerializeField] private MemoryRoom _bedroom;
    [SerializeField] private MemoryRoomFadeSequence _bedroomFadeSequence;

    [Header("Puzzle Parameters")]
    [SerializeField] private int _requiredDrumTaps = 3;

    [Header("Data")]
    [SerializeField] private BoolVariable[] _hasPieceFlagArray;
    [SerializeField] private IntVariable _drumTapCount;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPuzzleComplete;

    private void Awake()
    {
        foreach (BoolVariable hasPieceFlag in _hasPieceFlagArray)
        {
            hasPieceFlag.Value = false;
        }

        _drumTapCount.Value = 0;
    }

    private void OnEnable()
    {
        foreach (BoolVariable hasPieceFlag in _hasPieceFlagArray)
        {
            hasPieceFlag.ValueUpdated += CheckPiecesAcquired;
        }
        _drumTapCount.ValueUpdated += CheckDrumTaps;
    }

    private void OnDisable()
    {
        foreach (BoolVariable hasPieceFlag in _hasPieceFlagArray)
        {
            hasPieceFlag.ValueUpdated -= CheckPiecesAcquired;
        }
        _drumTapCount.ValueUpdated -= CheckDrumTaps;
    }

    private void CheckDrumTaps()
    {
        if (_drumTapCount.Value == _requiredDrumTaps)
        {
            HandDrum handDrum = _handDrumObject.GetComponent<HandDrum>();
            handDrum.Lower();
            _bedroomFadeSequence.CancelInvoke();
            for (int i = 0; i <= _bedroom.MaxPriority; i++)
            {
                _bedroom.FadeInAllInPriority(i);
            }
            _onPuzzleComplete?.Invoke();
            Debug.Log("Puzzle Finished!");
        }
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

        _handDrumObject.SetActive(true);
    }
}
