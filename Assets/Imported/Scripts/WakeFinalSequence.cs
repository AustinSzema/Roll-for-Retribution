using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeFinalSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _movePlayerTo;
    [SerializeField] private OpenDoorAndWalkSequence _joshOpenDoorAndWalk;

    [Header("UI Component References")]
    [SerializeField] private CanvasGroupFader _sceneFader;

    public void Begin()
    {
        StartCoroutine(WakeFinalRoutine());
    }

    private IEnumerator WakeFinalRoutine()
    {
        yield return _sceneFader.FadeRoutine(true);
        _playerController.CanMove = false;
        _characterController.enabled = false;
        _playerTransform.position = _movePlayerTo.position;
        _characterController.enabled = true;
        yield return _sceneFader.FadeRoutine(false);
        _joshOpenDoorAndWalk.Begin();
    }
}
