using UnityEngine;

/// <summary>
/// Basic state machine that switches which control prompts are shown to the 
/// player based on what they are currently controlling.
/// </summary>
public class ControlsDisplay : MonoBehaviour
{
    /// <summary>
    /// Variable from which to read the player's current POV.
    /// </summary>
    [Tooltip("Variable from which to read the player's current POV.")]
    [SerializeField] private PlayerPOVVariable _currentPOV;

    /// <summary>
    /// CanvasGroup containing the free fly camera controls display.
    /// </summary>
    [Tooltip("CanvasGroup containing the free fly camera controls display.")]
    [SerializeField] private CanvasGroup _freeFlyCameraControlsGroup;

    /// <summary>
    /// CanvasGroup containing the player controls display.
    /// </summary>
    [Tooltip("CanvasGroup containing the player controls display.")]
    [SerializeField] private CanvasGroup _playerControlsGroup;

    /// <summary>
    /// CanvasGroup containing the show controls prompt.
    /// </summary>
    [Tooltip("CanvasGroup containing the show controls prompt.")]
    [SerializeField] private CanvasGroup _showControlsGroup;

    /// <summary>
    /// Are there controls currently showing?
    /// </summary>
    private bool _controlsShowing = true;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        _currentPOV.VariableUpdated += SwitchControls;
    }
    private void Start()
    {
        HideControls();
    }
    private void OnDisable()
    {
        _currentPOV.VariableUpdated -= SwitchControls;
    }
    #endregion

    /// <summary>
    /// Toggles the visibility of the controls menu.
    /// </summary>
    public void ToggleControls()
    {
        if (_controlsShowing)
        {
            HideControls();
        }
        else
        {
            ShowControls();
        }
    }

    /// <summary>
    /// Hides both groups and shows the show controls display group.
    /// </summary>
    public void HideControls()
    {
        _playerControlsGroup.alpha = 0;
        _freeFlyCameraControlsGroup.alpha = 0;
        _showControlsGroup.alpha = 1;
        _controlsShowing = false;
    }

    /// <summary>
    /// Shows the control display based on what the player is currently 
    /// controlling.
    /// </summary>
    private void ShowControls()
    {
        if (_currentPOV.Value == PlayerPOV.FirstPerson ||
            _currentPOV.Value == PlayerPOV.ThirdPerson)
        {
            ShowPlayerControlsGroup();
        }
        else
        {
            ShowFreeFlyCameraControlsGroup();
        }
    }

    /// <summary>
    /// Shows the player control display group.
    /// </summary>
    private void ShowPlayerControlsGroup()
    {
        _playerControlsGroup.alpha = 1;
        _freeFlyCameraControlsGroup.alpha = 0;
        _showControlsGroup.alpha = 0;
        _controlsShowing = true;
    }

    /// <summary>
    /// Shows the free fly camera control display group.
    /// </summary>
    public void ShowFreeFlyCameraControlsGroup()
    {
        _playerControlsGroup.alpha = 0;
        _freeFlyCameraControlsGroup.alpha = 1;
        _showControlsGroup.alpha = 0;
        _controlsShowing = true;
    }

    /// <summary>
    /// If the controls are currently showing, and the player switches POV, this
    /// method switches which controls are displayed to the player.
    /// </summary>
    private void SwitchControls()
    {
        if (!_controlsShowing)
        {
            return;
        }

        ShowControls();
    }
}
