using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;

public class PuzzleObjectSlot : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _displayObject;
    [SerializeField] private AudioClip _placementFailedClip;
    [SerializeField] private AudioSource _playerAudioSource;

    [Header("Data")]
    [SerializeField] private BoolVariable _hasPieceFlag;
    [SerializeField] private BoolVariable _placedPieceFlag;

    private bool canPlayClip = true;

    public void PlacePuzzlePiece()
    {
        if (_hasPieceFlag.Value)
        {
            _displayObject.SetActive(true);
            _placedPieceFlag.Value = true;
        }
        else
        {
            if (canPlayClip)
            {
                _playerAudioSource.PlayOneShot(_placementFailedClip);
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
