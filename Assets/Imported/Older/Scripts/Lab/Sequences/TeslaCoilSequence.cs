using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoilSequence : MonoBehaviour
{
    [SerializeField] float _chargeTime = 1.0f;
    [SerializeField] float _flashDuration = 1.1f;
    [SerializeField] GameEvent _flashTriggerEvent;
    [SerializeField] GameEvent _chargeStartEvent;
    [SerializeField] GameEvent _chargeEndEvent;
    [SerializeField] GameEvent _playerDamageEvent;

    public void StartSequence()
    {
        StartCoroutine(TeslaCoilRoutine());
    }

    private IEnumerator TeslaCoilRoutine()
    {
        _chargeStartEvent.Raise();
        yield return new WaitForSeconds(_chargeTime);
        _flashTriggerEvent.Raise();
        _playerDamageEvent.Raise();
        yield return new WaitForSeconds(_flashDuration);
        _chargeEndEvent.Raise();
    }
}
