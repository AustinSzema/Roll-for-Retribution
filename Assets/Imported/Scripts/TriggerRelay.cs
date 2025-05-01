using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerRelay : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent _OnTriggerEnterResponse;
    [SerializeField] private UnityEvent _OnTriggerStayResponse;
    [SerializeField] private UnityEvent _OnTriggerExitResponse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            _OnTriggerEnterResponse?.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            _OnTriggerStayResponse?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            _OnTriggerExitResponse?.Invoke();
        }
    }
}
