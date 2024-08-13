using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EZDebug
{
    
    public static void Log(string msg)
    {
    #if  UNITY_EDTIOR
            Debug.Log(msg);        
    #endif
    }    
}
