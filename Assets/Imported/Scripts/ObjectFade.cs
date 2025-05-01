using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    /// <summary>
    /// The lower the priority, the sooner the object fades out. Set to 6 to 
    /// prevent fading entirely (i.e. for walls and floors).
    /// </summary>
    [Range(0, 5)]
    [Header("Priority")]
    [Tooltip("The lower the priority, the sooner the object fades out. Set " +
        "to 5 to prevent fading entirely (i.e. for walls and floors).")]
    public int FadePriority = 5;

    [Header("Component References")]
    [SerializeField] private Renderer[] _objectRenderers;
    [SerializeField] private Collider _objectCollider;

    [Header("Fade Parameters")]
    [SerializeField] private float _defaultFadeDuration = 5.0f;
    [SerializeField] private bool _retainShadows = false;

    private void Awake()
    {
        if (_objectCollider == null)
        {
            _objectCollider = GetComponent<Collider>();
        }
    }

    public void Hide()
    {
        StopAllCoroutines();
        foreach (Renderer objectRenderer in _objectRenderers)
        {
            foreach (Material material in objectRenderer.materials)
            {
                SetMaterialTransparent(material);
                material.color = new Color(material.color.r,
                    material.color.g, material.color.b, 0.0f);
            }
        }
        SetColliderActive(false);
    }

    public void In()
    {
        StopAllCoroutines();
        foreach (Renderer objectRenderer in _objectRenderers)
        {
            foreach (Material material in objectRenderer.materials)
            {
                StartCoroutine(FadeMaterialRoutine(material, true,
                    _defaultFadeDuration));
            }
        }
    }

    public void In(float fadeDuration)
    {
        StopAllCoroutines();
        foreach (Renderer objectRenderer in _objectRenderers)
        {
            foreach (Material material in objectRenderer.materials)
            {
                StartCoroutine(FadeMaterialRoutine(material, true, fadeDuration));
            }
        }
    }

    public void Out()
    {
        StopAllCoroutines();
        foreach (Renderer objectRenderer in _objectRenderers)
        {
            foreach (Material material in objectRenderer.materials)
            {
                StartCoroutine(FadeMaterialRoutine(material, false,
                    _defaultFadeDuration));
            }
        }
    }

    public void Out(float fadeDuration)
    {
        StopAllCoroutines();
        foreach (Renderer objectRenderer in _objectRenderers)
        {
            foreach (Material material in objectRenderer.materials)
            {
                StartCoroutine(FadeMaterialRoutine(material, false, fadeDuration));
            }
        }
    }

    public void Show()
    {
        StopAllCoroutines();
        foreach (Renderer objectRenderer in _objectRenderers)
        {
            foreach (Material material in objectRenderer.materials)
            {
                SetMaterialOpaque(material);
                material.color = new Color(material.color.r,
                    material.color.g, material.color.b, 1.0f);
            }
        }
        SetColliderActive(true);
    }

    private void SetColliderActive(bool active)
    {
        if (_objectCollider != null)
        {
            _objectCollider.enabled = active;
        }
    }

    private IEnumerator FadeMaterialRoutine(Material material, bool fadeIn, float fadeDuration)
    {
        if (!fadeIn)
        {
            SetMaterialTransparent(material);
        }
        else
        {
            SetColliderActive(true);
        }

        float elapsedTime = fadeIn ?
            (material.color.a * fadeDuration) :
            (1 - material.color.a) * fadeDuration;

        Color startColor = material.color;
        Color endColor = fadeIn ?
            new Color(startColor.r, startColor.g, startColor.b, 1.0f) :
            new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            material.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.color = endColor;

        if (fadeIn)
        {
            SetMaterialOpaque(material);
        }
        else
        {
            SetColliderActive(false);
        }
    }

    private void SetMaterialTransparent(Material material)
    {
        material.SetInt("_SrcBlend",
            (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend",
            (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.SetInt("_Surface", 1);

        material.renderQueue =
            (int)UnityEngine.Rendering.RenderQueue.Transparent;

        material.SetShaderPassEnabled("DepthOnly", false);
        material.SetShaderPassEnabled("SHADOWCASTER", _retainShadows);

        material.SetOverrideTag("RenderType", "Transparent");

        material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
    }

    private void SetMaterialOpaque(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.SetInt("_Surface", 0);

        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

        material.SetShaderPassEnabled("DepthOnly", true);
        material.SetShaderPassEnabled("SHADOWCASTER", true);

        material.SetOverrideTag("RenderType", "Opaque");

        material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
    }
}
