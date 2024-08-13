#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
public class SaveCameraImage : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveImage();
        }
    }

    public void SaveImage()
    {
        // Capture the camera's current rendering as a texture
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        _mainCam.targetTexture = renderTexture;
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        _mainCam.Render();
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _mainCam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Convert the texture to a PNG byte array
        byte[] bytes = screenshot.EncodeToPNG();
        
        // Load the saved image as a texture
        Texture2D savedImage = new Texture2D(2, 2);
        savedImage.LoadImage(bytes);

        // Create a new asset and save it to the Assets folder
        string assetPath = "Assets/Textures/Game Icon/Roll for Retribution Icon.png";
        
        File.WriteAllBytes(assetPath, bytes);

        // Import the asset to make it visible in the Editor
        AssetDatabase.ImportAsset(assetPath);

        // Refresh the Asset Database to reflect changes in the Editor
        AssetDatabase.Refresh();


        EZDebug.Log("Screenshot saved to: " + assetPath);
    }
    
}

#endif