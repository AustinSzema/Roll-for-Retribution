using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;
using UnityEngine.Events;

public class WakePuzzle : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private CandleLighter _candleLighter;
    [SerializeField] private MemoryRoom _wakeRoom;
    [SerializeField] private MemoryRoomFadeSequence _wakeRoomFadeSequence;

    [Header("Data")]
    [SerializeField] private BoolVariable[] _hasPieceFlagArray;
    [SerializeField] private BoolVariable[] _placedPieceFlagArray;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPuzzleFinished;

    private void Awake()
    {
        foreach (BoolVariable hasPieceFlag in _hasPieceFlagArray)
        {
            hasPieceFlag.Value = false;
        }

        foreach (BoolVariable placedPieceFlag in _placedPieceFlagArray)
        {
            placedPieceFlag.Value = false;
        }
    }

    private void Start()
    {
        _candleLighter.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (BoolVariable placedPieceFlag in _placedPieceFlagArray)
        {
            placedPieceFlag.ValueUpdated += CheckPiecesPlaced;
        }
        _candleLighter.ValueUpdated += FinishPuzzle;
    }

    private void OnDisable()
    {
        foreach (BoolVariable placedPieceFlag in _placedPieceFlagArray)
        {
            placedPieceFlag.ValueUpdated -= CheckPiecesPlaced;
        }
        _candleLighter.ValueUpdated -= FinishPuzzle;
    }

    private void FinishPuzzle()
    {
        _wakeRoomFadeSequence.CancelInvoke();
        for (int i = 0; i <= _wakeRoom.MaxPriority; i++)
        {
            _wakeRoom.FadeInAllInPriority(i);
        }
        _onPuzzleFinished?.Invoke();
        Debug.Log("Puzzle Finished!");
    }

    private void CheckPiecesPlaced()
    {
        foreach (BoolVariable placedPieceFlag in _placedPieceFlagArray)
        {
            if (placedPieceFlag.Value)
            {
                continue;
            }
            else
            {
                return;
            }
        }

        _candleLighter.gameObject.SetActive(true);
    }
}
