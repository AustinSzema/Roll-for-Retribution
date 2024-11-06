using System.Collections;
using UnityEngine;

public class EnemyLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int numPoints = 50;       // Number of points for the sine wave
    [SerializeField] private float frequency = 1.0f;   // Frequency of the sine wave
    [SerializeField] private float amplitude = 0.5f;   // Amplitude of the sine wave

    [SerializeField] private Gradient startGradient;
    [SerializeField] private Gradient endGradient;
    [SerializeField] private float fadeDuration = 2.0f; // Duration of the fade

    private Vector3 overlordPos;
    private Vector3[] points;

    private void OnEnable()
    {
        overlordPos = GameManager.Instance.overlordPosition;

        // Initialize points array for LineRenderer
        points = new Vector3[numPoints];
        lineRenderer.positionCount = numPoints;
        
        // Start the color fade effect
        StartCoroutine(FadeLine());
    }

    private IEnumerator FadeLine()
    {
        float elapsedTime = 0f;

        // Gradually transition from startGradient to endGradient over fadeDuration
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration); // Normalized time value (0 to 1)

            // Interpolate between the start and end gradients
            lineRenderer.colorGradient = LerpGradient(startGradient, endGradient, t);
            
            yield return null;
        }

        // Ensure the final gradient is set to endGradient
        lineRenderer.colorGradient = endGradient;
    }

    // Helper method to interpolate between two gradients
    private Gradient LerpGradient(Gradient start, Gradient end, float t)
    {
        Gradient lerpedGradient = new Gradient();

        // Lerp between color keys and alpha keys
        GradientColorKey[] colorKeys = new GradientColorKey[start.colorKeys.Length];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[start.alphaKeys.Length];

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = Color.Lerp(start.colorKeys[i].color, end.colorKeys[i].color, t);
            colorKeys[i].time = Mathf.Lerp(start.colorKeys[i].time, end.colorKeys[i].time, t);
        }

        for (int i = 0; i < alphaKeys.Length; i++)
        {
            alphaKeys[i].alpha = Mathf.Lerp(start.alphaKeys[i].alpha, end.alphaKeys[i].alpha, t);
            alphaKeys[i].time = Mathf.Lerp(start.alphaKeys[i].time, end.alphaKeys[i].time, t);
        }

        lerpedGradient.SetKeys(colorKeys, alphaKeys);
        return lerpedGradient;
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
