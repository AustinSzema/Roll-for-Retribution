using System;
using UnityEngine;

public class GrabDetection : MonoBehaviour
{
    [Header("Narrative")]
    public NarrativeSO narrativeSO;

    [Header("Audio")]
    public bool isTheRadio = false;
    public AudioSource audioSource;

    private Rigidbody rb;

    private bool canGrab = true;
    private bool grabbedBefore = false;
    private bool dayIsOver = false;


    private Collider[] cols;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cols = GetComponentsInChildren<Collider>();

        if (narrativeSO == null)
        {
            Debug.LogWarning($"{gameObject.name} is missing a NarrativeSO reference.");
        }

        if (isTheRadio && audioSource == null)
        {
            Debug.LogWarning($"{gameObject.name} is marked as a radio but missing an AudioSource.");
        }

        transform.parent = null;
    }

    private bool isGrabbed = false;
    void Update()
    {
        if (isGrabbed)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0f, 0f, 60f);
            transform.Rotate(0f, 180f, 0f);
            
        }
    }
    
    public void OnGrab()
    {
        foreach (Collider col in cols)
        {
            col.enabled = false;
            
        }
        isGrabbed = true;
        if (NarrativeTextSingleton.Instance.isFading || !canGrab) return;

        // Make kinematic while held
        rb.isKinematic = true;
        rb.useGravity = false;

        if (narrativeSO != null)
        {
            if (!grabbedBefore)
            {
                StaminaMeter.Instance.ReduceStamina(narrativeSO.staminaCost);
                grabbedBefore = true;
            }

            NarrativeTextSingleton.Instance.SetText(narrativeSO.description, narrativeSO.staminaCost);
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }

        canGrab = false;
    }

    public void OnRelease()
    {
        
        foreach (Collider col in cols)
        {
            col.enabled = true;
        }
        isGrabbed = false;
        if (NarrativeTextSingleton.Instance.isFading) return;

        // Unparent from hand
        transform.parent = null;

        // Re-enable Rigidbody physics

        rb.isKinematic = false;
        rb.useGravity = true;

        NarrativeTextSingleton.Instance.ClearText();

        if (!dayIsOver)
        {
            Debug.Log($"{gameObject.name} released");

            if (StaminaMeter.Instance.stamina <= 0)
            {
                dayIsOver = true;
                canGrab = false;
                NarrativeTextSingleton.Instance.StartNextDay();
            }
            else
            {
                canGrab = true;
            }
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }


    public void OnHoverEnter()
    {
        if (narrativeSO != null)
        {
            NarrativeTextSingleton.Instance.SetHighlightText(narrativeSO.GetObjectInfo());
        }
    }

    public void OnHoverExit()
    {
        Debug.Log($"{gameObject.name} highlight done");
        NarrativeTextSingleton.Instance.ClearHighlightText();
    }
}
