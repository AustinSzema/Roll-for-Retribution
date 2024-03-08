using UnityEngine;
using System.IO;
using UnityEditor;

public class SaveCameraImage : MonoBehaviour
{
    public Camera mainCamera; // Reference to the camera whose view you want to save

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
        mainCamera.targetTexture = renderTexture;
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        mainCamera.Render();
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Convert the texture to a PNG byte array
        byte[] bytes = screenshot.EncodeToPNG();

        // Save the PNG to a temporary location
        // string tempPath = Application.persistentDataPath + "/Roll for Retribution Icon.png";
        // File.WriteAllBytes(tempPath, bytes);
        //
        // Debug.Log("Temp file saved to: " + tempPath);

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


        Debug.Log("Screenshot saved to: " + assetPath);
    }
    
}