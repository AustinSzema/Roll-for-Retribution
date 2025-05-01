using UnityEngine;

/// <summary>
/// Allows designers to configure responses to OnTriggerEnter, OnTriggerStay,
/// and OnTriggerExit with a specific tag all in the inspector.
/// </summary>
public class TriggerDataRelay : MonoBehaviour
{
    [SerializeField] private TriggerResponse[] triggerResponses;

    #region MonoBehaviour Methods
    private void OnTriggerEnter(Collider other) 
    {
        foreach (TriggerResponse triggerResponse in triggerResponses)
        {
            if (other.transform.CompareTag(triggerResponse.Tag))
            {
                triggerResponse.OnTriggerEnterResponse?.Invoke();
                return;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        foreach (TriggerResponse triggerResponse in triggerResponses)
        {
            if (other.transform.CompareTag(triggerResponse.Tag))
            {
                triggerResponse.OnTriggerStayResponse?.Invoke();
                return;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (TriggerResponse triggerResponse in triggerResponses)
        {
            if (other.transform.CompareTag(triggerResponse.Tag))
            {
                triggerResponse.OnTriggerExitResponse?.Invoke();
                return;
            }
        }
    }
    #endregion
}
