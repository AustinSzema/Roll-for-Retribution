using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f; // Angle the door should rotate when opening
    public float duration = 1f; // Time it takes for the door to open/close
    public bool isOpen = false; // Whether the door is currently open or closed

    public AnimationComplete AnimationComplete;

    private Quaternion startRotation; // Starting rotation of the door
    private Quaternion endRotation; // End rotation of the door (when fully opened)

    void Awake()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(transform.eulerAngles.y, transform.eulerAngles.y + openAngle, transform.eulerAngles.z);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toggle();
        }
    }

    public void Open()
    {
        if (!isOpen)
        {
            StartCoroutine(AnimateDoor(endRotation, duration));
            isOpen = true;
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            StartCoroutine(AnimateDoor(startRotation, duration));
            isOpen = false;
        }
    }

    public void Toggle()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    private IEnumerator AnimateDoor(Quaternion targetRotation, float duration)
    {
        float elapsedTime = 0f;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        AnimationComplete?.Invoke();
    }
}