using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncToMainCamRotation : MonoBehaviour
{
    Transform cameratransform;

    private void Start()
    {
        cameratransform = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = cameratransform.rotation;
    }
}
