using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameEvent[] _sequenceStartEvents;
    [SerializeField] private Vector2 _attackSpacingRange;
    [SerializeField] private float _startDelayTime;

    public void Start()
    {
        Invoke("PickRandomAttack",_startDelayTime);
    }

    private void PickRandomAttack()
    {
        StartCoroutine(RandomAttackSequence());
    }

    private IEnumerator RandomAttackSequence()
    {
        _sequenceStartEvents[Random.Range(0, _sequenceStartEvents.Length)].Raise();
        float randomTime = Random.Range(_attackSpacingRange.x, _attackSpacingRange.y);
        yield return new WaitForSeconds(randomTime);
        Invoke("PickRandomAttack", 0.0f);
    }
}
