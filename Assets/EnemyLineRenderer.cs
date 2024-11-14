using System.Collections;
using UnityEngine;

public class EnemyLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer; 
    private int numPoints = 2;       // Number of points for the line

    [SerializeField] private Gradient startGradient;
    [SerializeField] private Gradient endGradient;
    [SerializeField] private float fadeDuration = 2.0f; // Duration of the fade

    private Vector3 overlordPos;
    private Vector3[] points;
    private GradientColorKey[] startColorKeys;
    private GradientAlphaKey[] startAlphaKeys;
    private GradientColorKey[] endColorKeys;
    private GradientAlphaKey[] endAlphaKeys;

    private void OnEnable()
    {
        if (numPoints < 2)
        {
            Debug.LogWarning("numPoints should be at least 2.");
            numPoints = 2;
        }

        lineRenderer.enabled = true;
        overlordPos = GameManager.Instance.overlordPosition;

        // Initialize points array for LineRenderer
        points = new Vector3[numPoints];
        lineRenderer.positionCount = numPoints;

        // Cache gradient keys to avoid accessing them frequently in Lerp
        startColorKeys = startGradient.colorKeys;
        startAlphaKeys = startGradient.alphaKeys;
        endColorKeys = endGradient.colorKeys;
        endAlphaKeys = endGradient.alphaKeys;

        // Start the color fade effect
        StartCoroutine(FadeLine());
    }

    private IEnumerator FadeLine()
    {
        float elapsedTime = 0f;
        Gradient lerpedGradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[startColorKeys.Length];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[startAlphaKeys.Length];

        // Gradually transition from startGradient to endGradient over fadeDuration
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration); // Normalized time value (0 to 1)

            // Only update color and alpha keys once per frame
            for (int i = 0; i < colorKeys.Length; i++)
            {
                colorKeys[i] = new GradientColorKey(
                    Color.Lerp(startColorKeys[i].color, endColorKeys[i].color, t),
                    Mathf.Lerp(startColorKeys[i].time, endColorKeys[i].time, t)
                );
            }

            for (int i = 0; i < alphaKeys.Length; i++)
            {
                alphaKeys[i] = new GradientAlphaKey(
                    Mathf.Lerp(startAlphaKeys[i].alpha, endAlphaKeys[i].alpha, t),
                    Mathf.Lerp(startAlphaKeys[i].time, endAlphaKeys[i].time, t)
                );
            }

            lerpedGradient.SetKeys(colorKeys, alphaKeys);
            lineRenderer.colorGradient = lerpedGradient;

            yield return null;
        }

        // Ensure the final gradient is set to endGradient
        lineRenderer.colorGradient = endGradient;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        // Retrieve the overlord position in each Update in case it changes
        overlordPos = GameManager.Instance.overlordPosition;

        points[0] = transform.position;
        points[1] = overlordPos;

        // Apply points to LineRenderer
        lineRenderer.SetPositions(points);
    }
}
