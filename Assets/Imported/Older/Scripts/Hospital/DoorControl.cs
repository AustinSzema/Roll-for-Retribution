using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Transform _doorPivot;
    [SerializeField] private float _closedYRotation = -90.0f;
    [SerializeField] private float _openYRotation = -180.0f;
    [SerializeField] private bool _doorOpen = false;
    [SerializeField] private BoolVariable _dependency;
    [SerializeField] private bool _isLocked = false;
    [SerializeField] private UnityEvent _firstAttemptResponse;
    [SerializeField] private UnityEvent _firstSuccessfulAttemptResponse;
    [SerializeField] private UnityEvent _moreAttemptResponse;
    [SerializeField] private UnityEvent _onDoorOpen;
    [SerializeField] private UnityEvent _onDoorClose;
    [SerializeField] private bool _isFirstAttempt = true;

    public void ToggleDoor()
    {
        if (_doorOpen)
        {
            CloseDoor();
            return;
        }
        else
        {
            OpenDoor();
            return;
        }
    }

    public void OpenDoor()
    {
        if (!_isLocked || _dependency.Value)
        {
            _firstSuccessfulAttemptResponse.Invoke();
            _isFirstAttempt = false;
            _doorPivot.localRotation = Quaternion.Euler(0.0f, _openYRotation, 0.0f);
            _doorOpen = true;
            _onDoorOpen.Invoke();
        }
        else
        {
            if (_isFirstAttempt)
            {
                _isFirstAttempt = false;
                _firstAttemptResponse.Invoke();
            }
            else
            {
                _moreAttemptResponse.Invoke();
            }
        }
    }

    public void CloseDoor()
    {
        _doorPivot.localRotation = Quaternion.Euler(0.0f, _closedYRotation, 0.0f);
        _doorOpen = false;
        _onDoorClose.Invoke();
    }
}
