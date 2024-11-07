using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    float cameraShakeFrequency;
    float camerShakeDuration;
    float cameraShakeMaxAngle;
    float cameraMagnitudeForXY;

    /*public void Start()
    {
        StartCoroutine(KeepShaking());
    }

    public IEnumerator KeepShaking()
    {
        while (true)
        {
            CameraShaker(cameraShakeFrequency, cameraShakeFrequency, cameraShakeAngle, cameraMagnitudeForXY);
            yield return new WaitForSeconds(3f);
        }
    }

    public void CameraShaker(float cameraShakeFrequency, float camerShakeDuration, float cameraShakeMaxAngle,
        float cameraMagnitudeForXY)
    {
        StartCoroutine(Shake(cameraShakeFrequency, camerShakeDuration, cameraShakeAngle, cameraMagnitudeForXY));
    }*/

    public IEnumerator Shake(float cameraShakeFrequency, float camerShakeDuration, float cameraShakeMaxAngle,
        float cameraMagnitudeForXY)
    {
        float elapsed = 0.0f;
        Vector3 originalPos = transform.localPosition;

        while (elapsed < camerShakeDuration)
        {
            float percentComplete = elapsed / camerShakeDuration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            float seed1 = Random.Range(0.1f, 0.5f) * Time.deltaTime * cameraShakeFrequency;
            float seed2 = Random.Range(0.1f, 0.5f) * Time.deltaTime * cameraShakeFrequency;
            float seed3 = Random.Range(0.1f, 0.5f) * Time.deltaTime * cameraShakeFrequency;

            float x = Mathf.PerlinNoise(seed1, 0f) * 2.0f - 1.0f;
            float y = Mathf.PerlinNoise(0f, seed2) * 2.0f - 1.0f;
            float shake = Mathf.PerlinNoise(seed3, 0f) * 2.0f - 1.0f;

            x *= cameraMagnitudeForXY * damper;
            y *= cameraMagnitudeForXY * damper;
            shake *= cameraShakeMaxAngle * damper;
            
            if (!GameManager.Instance.gameIsPaused)
            {
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, shake);
                transform.localPosition = originalPos + new Vector3(x, y, originalPos.z);
                //Debug.Log("transform " + transform.localPosition);
            }
            yield return null;
            elapsed += Time.deltaTime;
        }
        transform.localPosition = originalPos;
        transform.localRotation = Quaternion.identity;
    }
}

/*[SerializeField] private float cameraShakeMin = -1f;
    [SerializeField] private float cameraShakeMax = 1f;
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            if (!GameManager.Instance.gameIsPaused)
            {
                float x = Random.Range(cameraShakeMin, cameraShakeMax) * magnitude;
                float y = Random.Range(cameraShakeMin, cameraShakeMax) * magnitude;
                transform.localPosition = new Vector3(x, y, originalPos.z);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }*/
