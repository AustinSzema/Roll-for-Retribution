using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerSequence : MonoBehaviour
{
    [SerializeField] private float _flameDelayTime = 3.0f;
    [SerializeField] private float _flameTime = 2.5f;
    [SerializeField] private GameEvent _flamethrowerLowerEvent;
    [SerializeField] private GameEvent _flamethrowerRaiseEvent;
    [SerializeField] private GameEvent _flamethrowerStartEvent;
    [SerializeField] private GameEvent _flamethrowerEndEvent;
    [SerializeField] private GameEvent _damagePlayerEvent;
    
    public void StartSequence()
    {
        StartCoroutine(FlamethrowerRoutine());
    }

    private IEnumerator FlamethrowerRoutine()
    {
        _flamethrowerLowerEvent.Raise();
        yield return new WaitForSeconds(_flameDelayTime);
        _flamethrowerStartEvent.Raise();
        yield return new WaitForSeconds(_flameTime * 0.5f);
        _damagePlayerEvent.Raise();
        yield return new WaitForSeconds(_flameTime * 0.5f);
        _flamethrowerEndEvent.Raise();
        _flamethrowerRaiseEvent.Raise();
    }
}
