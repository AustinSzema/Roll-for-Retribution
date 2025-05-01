using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string _stringToDisplay;
    [SerializeField] private UnityEvent _OnInteractResponse;
    public bool IsTimeActivated;
    [SerializeField] private float _timeToActivate;

    public bool IsBeingLookedAt { get; set; }

    private float _elapsedTime;

    private void Start()
    {
        _elapsedTime = 0.0f;
    }

    private void Update()
    {
        if (IsBeingLookedAt)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _timeToActivate)
            {
                _OnInteractResponse?.Invoke();
            }
        }
    }

    public void Activate()
    {
        if (!IsTimeActivated)
        {
            _OnInteractResponse?.Invoke();
        }
    }

    public string GetInteractionString()
    {
        return _stringToDisplay;
    }
}
