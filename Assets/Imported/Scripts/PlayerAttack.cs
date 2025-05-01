using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private Camera mainCam;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private int damageAmount = 2;

    
    [SerializeField] private int ammoCount = 10;

    private int startingAmmo = 10;
    
    [SerializeField] private Animator animator;
    
    [SerializeField] private AudioSource gunAudio;

    [SerializeField] private AudioClip gunShotClip;
    [SerializeField] private AudioClip reloadClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!mainCam)
        {
            mainCam = Camera.main;
        }
        
        muzzleFlash.gameObject.SetActive(false);
        startingAmmo = ammoCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.instance.gameIsPaused)
        {
            
            RaycastHit hit;

            bool hoveredOverEnemy = Physics.Raycast(transform.position,
                mainCam.transform.TransformDirection(Vector3.forward), out hit,
                Mathf.Infinity,
                enemyLayerMask);

            LevelManager.instance.lookingAtEnemy = hoveredOverEnemy;
        
            if (hoveredOverEnemy)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);

                //Debug.Log("Did Hit");

            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //Debug.Log("Did not Hit");
            }


            if (Input.GetMouseButtonDown(0) && ammoCount > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            {
                StartCoroutine(ShowMuzzleFlash());
                gunAudio.pitch = Random.Range(0.95f, 1.05f);
                gunAudio.PlayOneShot(gunShotClip);
                if (hoveredOverEnemy)
                {
                    Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    if (enemy)
                    {
                        enemy.TakeDamage(damageAmount);
                    }
                }
            }

            else if(Input.GetMouseButtonDown(0) && ammoCount <= 0)
            {
                Reload();
            }

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }

        LevelManager.instance.ammoCountText.text = ammoCount + "/" + startingAmmo;
    }

    void Reload()
    {
        animator.SetTrigger("Reload");
        gunAudio.PlayOneShot(reloadClip);
        ammoCount = 10;        
    }

    private IEnumerator ShowMuzzleFlash()
    {
        ammoCount--;
        muzzleFlash.gameObject.SetActive(true);
        muzzleFlash.transform.Rotate(0f, 0f, Random.Range(45f, 180f)); 
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.gameObject.SetActive(false);
    }
}