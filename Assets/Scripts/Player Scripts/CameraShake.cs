using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    [SerializeField] private float cameraShakeMin = -1f;
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
    }
}
