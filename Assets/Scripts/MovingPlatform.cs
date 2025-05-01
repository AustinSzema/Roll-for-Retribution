using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{


    [SerializeField] private Rigidbody rb;

    [SerializeField] private float moveSpeed = 5f;

    public bool movingRight = true;

    private Vector3 moveDirection = new Vector3(1f, 0f, 0f);

    private void Start()
    {
        if (!movingRight)
        {
            moveDirection = new Vector3(moveDirection.x * -1, moveDirection.y, moveDirection.z);
        }
        moveDirection = moveDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection;
    }



}
