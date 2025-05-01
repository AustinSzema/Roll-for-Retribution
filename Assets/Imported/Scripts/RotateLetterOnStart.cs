using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLetterOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        
    }
}
