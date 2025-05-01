using UnityEngine;

public class FreezeTime : MonoBehaviour
{
    public void Freeze()
    {
        Time.timeScale = 0f;
    }

}
