using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingImage : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float blinkInterval = 0.5f; // Time interval between blinks in seconds
    private bool isBlinking = false;

    void Start()
    {
        // Start the blinking coroutine
        StartBlinking();
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
        while (true)
        {
            image.enabled = !image.enabled; // Toggle the image visibility
            yield return new WaitForSeconds(blinkInterval); // Wait for the blink interval
        }
    }
}