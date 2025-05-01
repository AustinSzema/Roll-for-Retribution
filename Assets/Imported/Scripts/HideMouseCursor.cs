using UnityEngine;

public class HideMouseCursor : MonoBehaviour
{
    private bool reachedGoal = false;
    // void Start()
    // {
    //     Cursor.visible = true; // Set the cursor to be initially visible
    // }

    void Update()
    {
        if (IsMouseOverGameWindow() || Input.GetMouseButtonDown(0))
        {
            if (!Goal.Instance.reachedGoal)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    bool IsMouseOverGameWindow()
    {
        Rect gameWindowRect = new Rect(0, 0, Screen.width, Screen.height);
        return gameWindowRect.Contains(Input.mousePosition);
    }
}
