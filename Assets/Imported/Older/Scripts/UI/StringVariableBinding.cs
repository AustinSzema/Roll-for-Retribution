using TMPro;
using UnityEngine;

public class StringVariableBinding : MonoBehaviour
{
    [SerializeField] private StringVariable _observedVariable;

    [SerializeField] private TextMeshProUGUI _displayText;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        _observedVariable.VariableUpdated += UpdateDisplayText;
    }
    private void Start()
    {
        UpdateDisplayText();
    }
    private void OnDisable()
    {
        _observedVariable.VariableUpdated -= UpdateDisplayText;
    }
    #endregion

    private void UpdateDisplayText()
    {
        _displayText.text = _observedVariable.Value;
    }
}
