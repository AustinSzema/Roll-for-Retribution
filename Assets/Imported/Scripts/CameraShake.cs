using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    
    public static CameraShake Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (cinemachineCamera == null)
        {
            Debug.LogError("CinemachineCamera is NOT assigned in CameraShake!");
            return;
        }

        noise = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        if (noise == null)
        {
            Debug.LogError("CinemachineBasicMultiChannelPerlin component is missing! Please add it to the Cinemachine Camera.");
        }
    }

    public void ShakeCamera(float intensity, float duration)
    {
        if (noise == null)
        {
            Debug.LogWarning("Cannot shake camera: No noise component found!");
            return;
        }

        StartCoroutine(ShakeRoutine(intensity, duration));
    }

    private IEnumerator ShakeRoutine(float intensity, float duration)
    {
        noise.AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        noise.AmplitudeGain = 0f;
    }
}