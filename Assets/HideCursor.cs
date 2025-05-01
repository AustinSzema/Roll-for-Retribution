using System;
using UnityEngine;

public class HideCursor : MonoBehaviour
{
    void Awake()
    {
    Cursor.visible = false;    
    }

    private void Update()
    {
        Cursor.visible = false;    
        
    }
}
