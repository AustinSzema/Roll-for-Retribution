using UnityEngine;

/// <summary>
/// Allows for easy showing and hiding of an attached CanvasGroup.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupRevealer : MonoBehaviour
{
    /// <summary>
    /// Group to control with this revealer.
    /// </summary>
    [SerializeField] private CanvasGroup _group;

    #region MonoBehaviour Methods
    private void Awake()
    {
        if (_group == null)
        {
            _group = GetComponent<CanvasGroup>();
        }
    }
    #endregion

    /// <summary>
    /// Hides the attached CanvasGroup.
    /// </summary>
    public void Hide()
    {
        _group.alpha = 0;
        _group.blocksRaycasts = false;
        _group.interactable = false;
    }

    /// <summary>
    /// Shows the attached CanvasGroup.
    /// </summary>
    public void Show()
    {
        _group.alpha = 1;
        _group.blocksRaycasts = true;
        _group.interactable = true;
    }
}
