using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
[ExecuteAlways]
public class MotionBlurTrail : MonoBehaviour
{
    private TrailRenderer trail;

    // You can tweak these values in the Inspector if you want
    [Header("Trail Settings")]
    public float time = 0.1f;
    public float startWidth = 0.3f;
    public float endWidth = 0.0f;
    public float minVertexDistance = 0.05f;
    public Gradient colorOverTime;

    private void OnValidate()
    {
        trail = GetComponent<TrailRenderer>();

        trail.time = time;
        trail.startWidth = startWidth;
        trail.endWidth = endWidth;
        trail.minVertexDistance = minVertexDistance;

        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        trail.receiveShadows = false;
        trail.alignment = LineAlignment.View;
        trail.numCapVertices = 0;

        if (colorOverTime == null || colorOverTime.colorKeys.Length == 0)
        {
            trail.colorGradient = DefaultGradient();
        }
        else
        {
            trail.colorGradient = colorOverTime;
        }
    }

    private Gradient DefaultGradient()
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.white, 0.0f),
                new GradientColorKey(Color.white, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.3f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        return gradient;
    }
}