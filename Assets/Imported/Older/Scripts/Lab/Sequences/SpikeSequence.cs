using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSequence : MonoBehaviour
{
    [SerializeField] private float _rumbleDuration = 3.0f;
    [SerializeField] private float _rumbleToSpikeRaiseDelay = 1.0f;
    [SerializeField] private GameEvent _spikeShakeStartEvent;
    [SerializeField] private GameEvent _spikeShakeEndEvent;
    [SerializeField] private GameEvent _raiseSpikesEvent;

    public void StartSequence()
    {
        StartCoroutine(SpikeRoutine());
    }

    private IEnumerator SpikeRoutine()
    {
        _spikeShakeStartEvent.Raise();
        yield return new WaitForSeconds(_rumbleDuration);
        _spikeShakeEndEvent.Raise();
        yield return new WaitForSeconds(_rumbleToSpikeRaiseDelay);
        _raiseSpikesEvent.Raise();
    }

    public float TotalSequenceTime()
    {
        return _rumbleDuration + _rumbleToSpikeRaiseDelay;
    }
}
