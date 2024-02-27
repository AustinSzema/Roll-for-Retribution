using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Magnet : MonoBehaviour
{
    private List<Rigidbody> _magneticObjects = new List<Rigidbody>();

    [SerializeField] private Transform _footPosition;
    [SerializeField] private Transform _handPosition;

    [SerializeField] private boolVariable _gameIsPaused;

    [Header("UI Images")] [SerializeField] private GameObject _attractImage;
    [SerializeField] private GameObject _repelImage;
    [SerializeField] private GameObject _defaultImage;

    [Header("Root Objects")] [SerializeField]
    private GameObject _attractParticlesRoot;

    [SerializeField] private GameObject _repelParticlesRoot;
    [SerializeField] private GameObject _gravityParticlesRoot;

    [Header("Particles")] [SerializeField] private ParticleSystem _repelParticles;
    [SerializeField] private ParticleSystem _gravityParticles;

    [Header("Weapon Values")] [SerializeField]
    private float _pullSpeed;

    [SerializeField] private float _slamSpeed;
    [SerializeField] private float _shotgunSpeed;
    [SerializeField] private float _reachThreshold = 1.0f;

    [SerializeField] private AudioManager _audioManager;

    [Header("Flight Ability")] public float _maxFlightDuration = 10;
    [SerializeField] private float _fuelDecrementAmount = 1;
    [SerializeField] private floatVariable _flightDuration;
    [SerializeField] private boolVariable _demonInHand;
    [SerializeField] private boolVariable _playerIsFlying;
    public float _minimumFuelAmount { get; private set; }

    private void Start()
    {
        // caches the reference to the audio manager at start
        _audioManager = FindObjectOfType<AudioManager>();

        /*for (int i = 0; i < 100; i++)
        {
            GameObject cube = Instantiate(_cubePrefab, transform.position + new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f)), Quaternion.identity);
            _magneticObjects.Add(cube.GetComponent<Rigidbody>());

        }*/
        _flightDuration.Value = _maxFlightDuration;

        Magnetic[] magneticObjects = FindObjectsOfType<Magnetic>();

        foreach (Magnetic magneticObject in magneticObjects)
        {
            _magneticObjects.Add(magneticObject.GetComponent<Rigidbody>());
        }

        _minimumFuelAmount = _maxFlightDuration / 4;
    }

    private bool _activateMagnet = false;

    private bool _outOfFuel = false;

    private void Update()
    {
        if (!_gameIsPaused.Value)
        {
            if (_flightDuration.Value <= 0f)
            {
                _outOfFuel = true;
            }

            // When the player is holding right mouse button and holding space and has demons in hand and has fuel
            if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.Space) && _demonInHand.Value &&
                _flightDuration.Value >= 0)
            {
                // if player is not out of fuel make them fly
                if (!_outOfFuel)
                {
                    transform.position = _footPosition.position;
                    _playerIsFlying.Value = true;
                    _audioManager.StartFlyingSound();
                    _flightDuration.Value -= _fuelDecrementAmount;
                }
            }
            else
            {
                _playerIsFlying.Value = false;
                transform.position = _handPosition.position;
            }

            // This makes it so that the player can't just hold the flight ability buttons and have the fuel go from 1 to 0 to 1 over and over again, giving them infinite flight.
            // Makes it so the fuel must regenerate a bit before the player can use flight again 

            if (_flightDuration.Value >= _minimumFuelAmount)
            {
                _outOfFuel = false;
            }

            // if the player is out of fuel stop flying
            if (_flightDuration.Value <= 0)
            {
                _outOfFuel = true;
                transform.position = _handPosition.position;
                _playerIsFlying.Value = false;
                _audioManager.StopFlyingSound();
            }

            // if the player's fuel is not full and the player is not flying, fill up their fuel
            if (_flightDuration.Value < _maxFlightDuration && !_playerIsFlying.Value)
            {
                _flightDuration.Value += _fuelDecrementAmount / 3;
            }

            // TODO: Consider renaming _flightDuration to _flightFuel


            // Debug.Log("Flight Duration: " + _flightDuration.Value + ", is grounded: " + _playerIsGrounded.Value);

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

            if (Input.GetMouseButtonUp(1))
            {
                _activateMagnet = false;
                _audioManager.StopPullingSound();
                /*foreach (Rigidbody rb in _magneticObjects)
                {
                    transform.position = _explodePosition.position;
                    rb.AddExplosionForce(20000f, transform.position, 100f, 0.0F);
                }*/
                _repelImage.SetActive(false);
                _attractImage.SetActive(false);
                _defaultImage.SetActive(true);
                _attractParticlesRoot.SetActive(false);
            }

            //shotgun push
            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            {
                _audioManager.PlayShotgunSound();
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
                _audioManager.PlaySlamSound();
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_activateMagnet)
        {
            foreach (Rigidbody obj in _magneticObjects)
            {
                // calculate distance from object
                float distance = Vector3.Distance(obj.position, transform.position);

                obj.velocity = Vector3.zero;
                obj.position = Vector3.MoveTowards(obj.transform.position, transform.position,
                    Time.deltaTime * _pullSpeed);

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