using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternVisualizer : MonoBehaviour
{
    [SerializeField] private PatternSerializer patternSerializer;
    
    private void OnDrawGizmosSelected()
    {
        if (patternSerializer != null)
        {
            patternSerializer.DrawPatternPoints();
        }
    }
}
