using System;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class TakeScreenshot : MonoBehaviour
{
    [SerializeField] private KeyCode inputKey;

    [SerializeField] private bool takeScreenshotOnStart = false;

    private void Start()
    {
        if (takeScreenshotOnStart)
        {
            TakeAScreenshot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputKey)) 
        {
            TakeAScreenshot();
        }
    }

    private void TakeAScreenshot()
    {
        
        string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "Assets/Screenshots/DemonTimeScreenshot_" + time + ".png";

        Debug.Log("Screenshot saved as: " + fileName);
            
        ScreenCapture.CaptureScreenshot(fileName);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}