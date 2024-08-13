using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainExpansion : MonoBehaviour
{
    [SerializeField] private float _expansionSpeed = 2f;

    private bool _expandDomain = false;

    [SerializeField] private float _domainSizeValue = 200f;
    private Vector3 _domainSize = Vector3.one;

    private Vector3 _originalSize = Vector3.one;

    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private ParticleSystem _domainParticles;
    
    private void Start()
    {
        _domainSize = new Vector3(_domainSizeValue, _domainSizeValue, _domainSizeValue);
        _originalSize = transform.localScale;
        _meshRenderer.enabled = false;
        _domainParticles.Stop();
    }
    // Update is called once per frame
    void Update()
    {

        if (_expandDomain)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * 100f, 0f));
        }

        float transformAverage = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3f;

        if (_expandDomain && _domainSizeValue - transformAverage > 0)
        {
            //transform.localScale = Vector3.Lerp(transform.localScale, _domainSize, Time.deltaTime * _expansionSpeed);
            transform.localScale += new Vector3(Time.deltaTime * _expansionSpeed, Time.deltaTime * _expansionSpeed, Time.deltaTime * _expansionSpeed);
        }

        if (_domainSizeValue - transformAverage <= 0)
        {
            StartCoroutine(ShrinkDomain(1f));
            EZDebug.Log("Begun shrinking domain");

        }


        /*
        if (Vector3.Distance(transform.localScale, _domainSize) <= 10f)
        {
            StartCoroutine(ShrinkDomain(20f));
        }*/
    }

    public void ExpandDomain()
    {
        
        _meshRenderer.enabled = true;
        transform.localScale = _originalSize;
        _expandDomain = true;
            
        _domainParticles.Play();

    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.GetComponent<PlayerController>() == null)
        {
            other.gameObject.GetComponent<IDamageable>().takeDamage(1);
        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.GetComponent<PlayerController>() == null)
    //     {
    //         other.gameObject.GetComponent<IDamageable>().takeDamage(1);
    //     }
    // }

    /*private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.GetComponent<PlayerController>() == null)
        {
            other.gameObject.GetComponent<IDamageable>().takeDamage(1);
        }
    }*/

    private IEnumerator ShrinkDomain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        EZDebug.Log("shrunk domain");
        transform.localScale = _originalSize;
        _expandDomain = false;
        _meshRenderer.enabled = false;
        _domainParticles.Stop();
    }
    
}