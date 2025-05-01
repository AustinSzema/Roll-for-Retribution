using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggerResponse
{
    /// <summary>
    /// Which tag should this trigger response detect?
    /// </summary>
    [Tooltip("Which tag should this trigger response detect?")]
    public string Tag;
    public UnityEvent OnTriggerEnterResponse;
    public UnityEvent OnTriggerStayResponse;
    public UnityEvent OnTriggerExitResponse;
}
