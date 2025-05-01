using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFloor : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void TriggerSpikes()
    {
        _animator.SetTrigger("RaiseSpikes");
    }
}
