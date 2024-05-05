using System;
using UnityEngine;
using UnityEngine.UI;

public class PulseOutline : MonoBehaviour
{
    [SerializeField] private Outline _outline;
    [SerializeField] private float _pulseSpeed = 1.0f;
    private float _maxEffectDistanceX = -15.0f;
    private float _maxEffectDistanceY = 15.0f;

    private Vector2 _startingEffectDistance;

    private void Start()
    {
        _startingEffectDistance = _outline.effectDistance;
        _maxEffectDistanceX = _outline.effectDistance.x;
        _maxEffectDistanceY = _outline.effectDistance.y;
    }

    void Update()
    {
        // Calculate pulse using sine function for smooth oscillation
        float pulse = Mathf.Sin(Time.time * _pulseSpeed);
        
        // Map pulse value to effect distance within the range [0, maxEffectDistance]
        float newX = Mathf.Lerp(0, _maxEffectDistanceX, pulse);
        float newY = Mathf.Lerp(0, _maxEffectDistanceY, pulse);

        // Update outline effect distance
        _outline.effectDistance = new Vector2(newX, newY);
    }
}