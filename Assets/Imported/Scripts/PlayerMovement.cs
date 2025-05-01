using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;


    private CharacterController controller;
    private Camera playerCamera;
    private float xRotation = 0f;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded;

    public LayerMask groundLayerMask;

    private bool isFrozen = false;


    void Start()
    {

        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2f);
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (!LevelManager.instance.gameIsPaused && !isFrozen)
        {
            // Mouse look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

            // Movement
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            controller.Move(moveSpeed * Time.deltaTime * move);

            // Jumping and gravity
            isGrounded = IsGrounded();

            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }


    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.25f, groundLayerMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckForInteractable(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckForInteractable(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckForInteractable(other.gameObject);
    }


    private void CheckForInteractable(GameObject other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
    public void Freeze()
    {
        isFrozen = true;
        velocity = Vector3.zero; // Optional: stop vertical movement

    }

}