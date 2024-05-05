using UnityEngine;

public class CubeFollowMouse : MonoBehaviour
{
    // Adjust this value to set the speed of the cube
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Get the current position of the mouse cursor
        Vector3 mousePos = Input.mousePosition;
        
        // Convert the mouse position from screen space to world space
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f)); // 10f is the distance of the camera from the scene
        
        // Ensure that the z-coordinate of the mouse position matches the z-coordinate of the cube
        mousePos.z = transform.position.z;

        // Move the cube towards the mouse cursor
        transform.position = mousePos;
    }
}
