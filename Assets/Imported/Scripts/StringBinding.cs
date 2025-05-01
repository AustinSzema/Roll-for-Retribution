using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using TMPro;
using UnityEngine;

public class StringBinding : MonoBehaviour
{
    [SerializeField] private StringVariable _observedString;
    [SerializeField] private TextMeshProUGUI _boundText;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        _observedString.ValueUpdated += UpdateBoundText;
    }
    private void Start()
    {
        UpdateBoundText();
    }
    private void OnDisable()
    {
        _observedString.ValueUpdated -= UpdateBoundText;
    }
    #endregion

    private void UpdateBoundText()
    {
        _boundText.text = _observedString.Value;
    }
}
