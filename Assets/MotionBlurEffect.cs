using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MotionBlurEffect : MonoBehaviour
{
    public Shader motionBlurShader;
    private Material motionBlurMaterial;

    [Range(0.0f, 1.0f)]
    public float blurIntensity = 0.5f;
    [Range(0.0f, 1.0f)]
    public float maxBlurDistance = 1.0f;

    void OnEnable()
    {
        if (motionBlurShader == null)
        {
            Debug.LogError("MotionBlurShader not assigned.");
            return;
        }

        motionBlurMaterial = new Material(motionBlurShader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (motionBlurMaterial != null)
        {
            motionBlurMaterial.SetFloat("_Intensity", blurIntensity);
            motionBlurMaterial.SetFloat("_MaxBlurDistance", maxBlurDistance);
            Graphics.Blit(source, destination, motionBlurMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    void OnDisable()
    {
        if (motionBlurMaterial != null)
        {
            DestroyImmediate(motionBlurMaterial);
        }
    }
}