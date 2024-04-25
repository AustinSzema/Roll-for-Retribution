using System;
using UnityEngine;
using UnityEngine.UI;

public class PulseOutline : MonoBehaviour
{
    [SerializeField] private Outline _outline;
    [SerializeField] private float _pulseSpeed = 1.0f;
    [SerializeField] private float _maxEffectDistance = 2.0f;
    private Vector2 _startingEffectDistance;

    private void Start()
    {
        _startingEffectDistance = _outline.effectDistance;
    }

    private void OnEnable()
    {
        _outline.effectDistance = Vector2.zero;        
    }

    void Update()
    {
        // Calculate pulse using sine function for smooth oscillation
        float pulse = Mathf.Sin(Time.time * _pulseSpeed);
        
        // Map pulse value to effect distance within the range [0, maxEffectDistance]
        float newX = Mathf.Lerp(0, _maxEffectDistance, pulse);
        float newY = Mathf.Lerp(0, _maxEffectDistance, pulse);

        // Update outline effect distance
        _outline.effectDistance = new Vector2(newX, newY);
    }
}