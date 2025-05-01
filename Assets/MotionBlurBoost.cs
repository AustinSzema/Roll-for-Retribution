using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MotionBlurBoost : MonoBehaviour
{
    public Volume volume;
    private MotionBlur motionBlur;

    void Start()
    {
        if (volume.profile.TryGet(out motionBlur))
        {
            motionBlur.intensity.Override(10f); // 0 to 1
            motionBlur.clamp.Override(0f); // Lower means more blur
        }
    }
}