using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingElement : MonoBehaviour
{
    [SerializeField] private Graphic _blinkingGraphic; // Use Graphic for both TextMeshProUGUI and Images
    private float blinkInterval = 0.25f; // Time interval between blinks in seconds
    private bool isBlinking = false;

    private void OnEnable()
    {
        StartBlinking();
    }

    private void OnDisable()
    {
        StopBlinking();
    }

    void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }
    }

    void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        while (isBlinking)
        {
            _blinkingGraphic.enabled = !_blinkingGraphic.enabled; // Toggle the visibility of the Graphic component
            yield return new WaitForSeconds(blinkInterval); // Wait for the blink interval
        }
    }
}
