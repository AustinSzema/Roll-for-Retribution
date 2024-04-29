using System;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
    [SerializeField] private KeyCode inputKey;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputKey)) 
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string fileName = "Assets/Screenshots/DemonTimeScreenshot_" + time + ".png";

            Debug.Log("Screenshot saved as: " + fileName);
            
            ScreenCapture.CaptureScreenshot(fileName);
        }
    }
}