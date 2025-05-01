using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO
 * 
 * Should the game be a not moving shooter game?
 * Add delay to shooting
 * Make the gun explosion affect the player
 * 
 * 
 * 
 */

// M-0,0

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private Transform gunEnd;
    [SerializeField] private float weaponRange;
    [SerializeField] private float shotDuration = 1f;    // WaitForSeconds object used by our ShotEffect coroutine, 
    [SerializeField] private LayerMask shootableLayers;
    [SerializeField] private Material lineRendererMaterial;


    private bool _hitSomething = true;



    void Update()
    {

        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetMouseButtonDown(0) && lineRendererMaterial.GetFloat("_Alpha") <= 0.1f || Input.GetMouseButtonDown(0) && _hitSomething)
        {
            lineRendererMaterial.SetFloat("_Alpha", 1f);

            // Start our ShotEffect coroutine to turn our laser line on and off
            //StartCoroutine(ShotEffect());

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, gunEnd.position);


            // Check if our raycast has hit anything
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, Mathf.Infinity, shootableLayers))
            {
                // Set the end position for our laser line 
                laserLine.SetPosition(1, hit.point);
                if(hit.transform.gameObject.GetComponent<IDamageable>() != null)
                {
                    hit.transform.gameObject.GetComponent<IDamageable>().takeDamage(1);
                }
                Debug.Log(hit.transform.gameObject);

                if (hit.transform.gameObject.CompareTag("Cow"))
                {
                    _hitSomething = true;
                    Debug.Log("hit cow");
                }
                else
                {
                    _hitSomething = false;
                }


            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                laserLine.SetPosition(1, rayOrigin + (mainCam.transform.forward * weaponRange));
            }
        }
        lineRendererMaterial.SetFloat("_Alpha", lineRendererMaterial.GetFloat("_Alpha") - Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
        {

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;


            // Check if our raycast has hit anything
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, Mathf.Infinity, shootableLayers))
            {
                // Set the end position for our laser line 
                laserLine.SetPosition(1, hit.point);
                if (hit.transform.gameObject.GetComponent<IDamageable>() != null)
                {
                    hit.transform.gameObject.GetComponent<IDamageable>().takeDamage(1);
                }
                Debug.Log(hit.transform.gameObject);


            }
        }
    }


    void OnApplicationQuit()
    {
        lineRendererMaterial.SetFloat("_Alpha", 1f);
    }

}