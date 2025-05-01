using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    private bool _isCursorVisible = true;

    private void Start()
    {
        // Show the cursor at the beginning of the game.
        Cursor.visible = _isCursorVisible;
    }

    private void Update()
    {
        // Check for user input to toggle cursor visibility.
        if (Input.GetMouseButtonDown(0)) // Change this condition to suit your interaction trigger.
        {
            _isCursorVisible = false;

            // Lock or unlock the cursor to the center of the game window.
            Cursor.lockState = _isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
