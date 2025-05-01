using System.Collections;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] private float shakeAmplitude = 1.0f;
    [SerializeField] private float shakeFrequency = 1.0f;
    [SerializeField] private float shakeFadeTime = 0.25f;
    [SerializeField] private CinemachineVirtualCamera _virtualCam;

    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float _elapsedTime;

    private void Awake()
    {
        cinemachineBasicMultiChannelPerlin =
            _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void StartShake()
    {
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = shakeFrequency;
        StartCoroutine(FadeOutShakeRoutine());
    }

    public void StopShake()
    {
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
    }

    private IEnumerator FadeOutShakeRoutine()
    {
        _elapsedTime = 0;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeAmplitude;

        while (_elapsedTime < shakeFadeTime)
        {
            _elapsedTime += Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain =
                Mathf.Lerp(shakeFrequency, 0.0f, _elapsedTime / shakeFadeTime);
            yield return null;
        }

        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
    }
}
