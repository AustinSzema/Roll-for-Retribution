using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Check if our raycast has hit anything
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, Mathf.Infinity))
            {
                playerTransform.position = hit.point;
            }
        }
    }
}
