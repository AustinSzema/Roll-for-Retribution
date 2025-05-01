using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintKeyInputs : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyInputsText;

    private string _keyText;

    // Update is called once per frame
    void Update()
    {
        _keyText = "";
        // Check for keyboard keys
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(keyCode))
            {
                _keyText += " + " + keyCode;
            }
        }

        if (_keyText.IndexOf("+") > 0)
        {
            _keyInputsText.text = "Key Inputs: " + _keyText.Remove(_keyText.IndexOf("+"), 1);
        }
    }
    
}
