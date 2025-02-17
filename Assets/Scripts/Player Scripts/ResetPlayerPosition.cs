using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody rb;

    private Vector3 originalPos;
    private Quaternion originalRotation;

    private void Start()
    {
        originalPos = playerTransform.position;
        originalRotation = playerTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.y <= -25f)
        {
            playerTransform.position = originalPos;
            playerTransform.rotation = originalRotation;
            rb.linearVelocity = Vector3.zero;

        }
    }

}
