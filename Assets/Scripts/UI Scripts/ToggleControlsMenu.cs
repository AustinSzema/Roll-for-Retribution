using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleControlsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _controlsCanvas;

    private bool _toggleCanvas = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _toggleCanvas = !_toggleCanvas;
        }

        if (_toggleCanvas)
        {
            _controlsCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _controlsCanvas.SetActive(false);
            Time.timeScale = 1;

        }
    }
}
