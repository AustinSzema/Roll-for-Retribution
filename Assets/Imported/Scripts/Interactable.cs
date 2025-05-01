using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string ItemName;
    [SerializeField] private UnityEvent OnInteract;
    [SerializeField] private UnityEvent OnHoverStart;
    [SerializeField] private UnityEvent OnHoverEnd;

    private bool _currentlyHovered = false;

    public void StartHover()
    {
        if (!_currentlyHovered)
        {
            _currentlyHovered = true;
            OnHoverStart?.Invoke();
        }
    }

    public void EndHover()
    {
        if (_currentlyHovered)
        {
            _currentlyHovered = false;
            OnHoverEnd?.Invoke();
        }
    }

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
