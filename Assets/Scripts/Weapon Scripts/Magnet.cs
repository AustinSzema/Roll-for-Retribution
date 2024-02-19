using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Magnet : MonoBehaviour
{
    private List<Rigidbody> _magneticObjects = new List<Rigidbody>();

    [SerializeField] private Transform _footPosition;

    [SerializeField] private Transform _handPosition;

    [SerializeField] private GameObject _attractImage;
    [SerializeField] private GameObject _repelImage;
    [SerializeField] private GameObject _defaultImage;

    [SerializeField] private GameObject _attractParticlesRoot;
    [SerializeField] private GameObject _repelParticlesRoot;
    [SerializeField] private ParticleSystem _repelParticles;
    [SerializeField] private GameObject _gravityParticlesRoot;
    [SerializeField] private ParticleSystem _gravityParticles;
    
    [SerializeField] private float _pullSpeed;
    [SerializeField] private float _slamSpeed;
    [SerializeField] private float _shotgunSpeed;
    [SerializeField] private float _reachThreshold = 1.0f;

    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        /*for (int i = 0; i < 100; i++)
        {
            GameObject cube = Instantiate(_cubePrefab, transform.position + new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f)), Quaternion.identity);
            _magneticObjects.Add(cube.GetComponent<Rigidbody>());
            
        }*/

        // caches the reference to the audio manager at start
        _audioManager = FindObjectOfType<AudioManager>();
        
        Magnetic[] magneticObjects = GameObject.FindObjectsOfType<Magnetic>();

        // Do something with each object that has the Magnetic script
        foreach (Magnetic magneticObject in magneticObjects)
        {
            // Perform actions on each object, for example:
            _magneticObjects.Add(magneticObject.GetComponent<Rigidbody>());
        }
        

    }

    private bool _activateMagnet = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = _footPosition.position;
        }
        else
        {
            transform.position = _handPosition.position;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            _activateMagnet = true;
            _audioManager.StartPullingSound();
            transform.position = _handPosition.position;
            _repelImage.SetActive(false);
            _attractImage.SetActive(true);
            _defaultImage.SetActive(false);
            _attractParticlesRoot.SetActive(true);
        }
        if(Input.GetMouseButtonUp(1))
        {
            _activateMagnet = false;
            /*foreach (Rigidbody rb in _magneticObjects)
            {
                transform.position = _explodePosition.position;
                rb.AddExplosionForce(20000f, transform.position, 100f, 0.0F);
            }*/
            _audioManager.StopPullingSound();
            _repelImage.SetActive(false);
            _attractImage.SetActive(false);
            _defaultImage.SetActive(true);
            _attractParticlesRoot.SetActive(false);

        }
        
        //shotgun push
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            foreach (Rigidbody rb in _magneticObjects)
            {
                _attractImage.SetActive(false);
                _repelImage.SetActive(true);
                _defaultImage.SetActive(false);
                _attractParticlesRoot.SetActive(false);
                _repelParticlesRoot.SetActive(true);
                _repelParticles.Clear();
                _repelParticles.Play();
                rb.AddForce(transform.forward * _shotgunSpeed);
                _activateMagnet = false;
            }
        }
        
        //slam 
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(2))
        {
            foreach (Rigidbody rb in _magneticObjects)
            {
                _attractImage.SetActive(false);
                _repelImage.SetActive(true);
                _defaultImage.SetActive(false);
                _attractParticlesRoot.SetActive(false);
                _gravityParticlesRoot.SetActive(true);
                _gravityParticles.Clear();
                _gravityParticles.Play();
                
                rb.AddForce(Vector3.down * _slamSpeed);
                
                _activateMagnet = false;
            }
        }



        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_activateMagnet)
        {
            foreach (Rigidbody obj in _magneticObjects )
            {
                // calculate distance from object
                float distance = Vector3.Distance(obj.position, transform.position);

                obj.velocity = Vector3.zero;
                obj.position = Vector3.MoveTowards(obj.transform.position, transform.position, Time.deltaTime * _pullSpeed);

                if (distance <= _reachThreshold)
                {
                    _audioManager.PlayObjectReachedSound();
                }

                /*// Calculate the direction from the current position to the target position
                Vector3 direction = (transform.position - obj.position).normalized;

                // Set the velocity of the Rigidbody in the calculated direction
                obj.velocity = Time.fixedDeltaTime * 2000f * direction;*/
            }
            
        }
    }
}
