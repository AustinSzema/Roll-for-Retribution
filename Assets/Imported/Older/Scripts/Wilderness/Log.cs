using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] private BoolVariable _playerHasLogVariable;
    [SerializeField] private GameEvent _logPickupFailedEvent;

    public void TryPickupLog()
    {
        if (_playerHasLogVariable.Value)
        {
            _logPickupFailedEvent.Raise();
            return;
        }
        else
        {
            _playerHasLogVariable.Value = true;
            Destroy(gameObject);
        }
    }
}
