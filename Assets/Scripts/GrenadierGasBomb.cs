using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrenadierGasBomb : MonoBehaviour
{
    private float scale = 50f;
    [SerializeField] private Vector3 expandSize = new Vector3(10f, 10f, 10f);
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float shootForce = 100f;
    [SerializeField] private float shootUpwardArc = 30f;
    [SerializeField] private int shrinkDelay;
    [SerializeField] private float shrinkSpeedMultiplier;
    public AudioSource audioSource;
    public AudioClip fireSFX;
    public AudioClip explodeSFX;

    private void Start()
    {
        PlayGrenadeFireSfx();
        expandSize = new Vector3(scale, scale, scale);
        rb.velocity = Vector3.zero;
        transform.position = transform.position + Vector3.forward * 5f;
        
        Vector3 playerVelocity = GameManager.Instance.playerRigidBodyVelocity;
        float timeToReach = Vector3.Distance(GameManager.Instance.playerPosition, transform.position) / shootForce;
        Vector3 futurePlayerPosition = GameManager.Instance.playerPosition + playerVelocity * timeToReach;
        Vector3 directionToFuturePosition = (futurePlayerPosition - transform.position).normalized;
        rb.velocity = directionToFuturePosition * shootForce;
        
        //Vector3 directionToPlayer = (GameManager.Instance.playerPosition - transform.position).normalized;
        //rb.velocity = directionToPlayer * shootForce;
        rb.velocity += Vector3.up * shootUpwardArc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(TagManager.enemyTag))
        {
            PlayGrenadeExplodeSfx();
            rb.velocity = Vector3.zero;
            transform.localScale = expandSize;
            rb.isKinematic = true;
            StartCoroutine(WaitAndShrink());
        }
    }

    private IEnumerator Shrink()
    {
        while (transform.localScale.x > 0.1f)
        {
            if (!GameManager.Instance.gameIsPaused)
            {
                transform.localScale *= shrinkSpeedMultiplier;
                yield return new WaitForSeconds(0f);
            }
            else
            {
                yield return null;
            }
        }
        Destroy(gameObject);
    }
    private IEnumerator WaitAndShrink()
    {
        yield return new WaitForSeconds(shrinkDelay); 
        StartCoroutine(Shrink());
    }

    void PlayGrenadeFireSfx()
    {
        if (audioSource != null && fireSFX != null)
        {
            audioSource.clip = fireSFX;
            audioSource.Play();
        }
    }
    
    void PlayGrenadeExplodeSfx()
    {
        if (audioSource != null && explodeSFX != null)
        {
            audioSource.clip = explodeSFX;
            audioSource.Play();
        }
    }
    
}