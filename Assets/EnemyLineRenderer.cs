using System.Collections;
using UnityEngine;

public class EnemyLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int numPoints = 50;       // Number of points for the sine wave
    [SerializeField] private float frequency = 1.0f;   // Frequency of the sine wave
    [SerializeField] private float amplitude = 0.5f;   // Amplitude of the sine wave

    private Vector3 overlordPos;
    private Vector3[] points;

    private void OnEnable()
    {
        overlordPos = GameObject.Find("Overlord").transform.position;

        // Initialize points array for LineRenderer
        points = new Vector3[numPoints];
        lineRenderer.positionCount = numPoints;
        
    }

    private void Update()
    {
        // Calculate direction from enemy to overlord
        Vector3 startPos = transform.position;
        Vector3 direction = (overlordPos - startPos).normalized;
        float distance = Vector3.Distance(startPos, overlordPos);

        // Generate sine wave points between the enemy and the overlord
        for (int i = 0; i < numPoints; i++)
        {
            // Linear interpolation between startPos and overlordPos
            float t = (float)i / (numPoints - 1);
            Vector3 pointPosition = Vector3.Lerp(startPos, overlordPos, t);

            // Calculate sine wave offset
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized;
            float sineOffset = Mathf.Sin(t * distance * frequency * Mathf.PI * 2) * amplitude;
            pointPosition += perpendicular * sineOffset;

            // Set the position in the LineRenderer
            points[i] = pointPosition;
        }

        // Apply points to LineRenderer
        lineRenderer.SetPositions(points);
    }
}