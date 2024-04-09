using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

// TODO: Does this script need to be broken up? It has so many references
public class Magnet : MonoBehaviour
{
    // Stores a reference to all of the demon dice
    private List<Rigidbody> _magneticObjects = new List<Rigidbody>();

    [Header("Magnet Positions")]
    [SerializeField] private Transform _footPosition;
    [SerializeField] private Transform _handPosition;

    [Header("SO Variables")]
    [SerializeField] private boolVariable _gameIsPaused;
    [SerializeField] private boolVariable _outOfFuel;
    [SerializeField] private boolVariable _pullingInDemons;
    [SerializeField] private ShotTypeSO _activeShotTypeSO;
    
    
    [Header("UI Images")] [SerializeField] private GameObject _attractImage;
    [SerializeField] private GameObject _repelImage;
    [SerializeField] private GameObject _defaultImage;

    [Header("Root Objects")]
    [SerializeField] private GameObject _attractParticlesRoot;
    [SerializeField] private GameObject _repelParticlesRoot;
    [SerializeField] private GameObject _gravityParticlesRoot;

    [Header("Particles")]
    [SerializeField] private Renderer _attractParticlesRenderer;
    [SerializeField] private ParticleSystem _repelParticles;
    [SerializeField] private ParticleSystem _gravityParticles;
    [SerializeField] private MeshRenderer _centerSphere;
    [SerializeField] private MeshRenderer _outerSphere;
    [SerializeField] private Material _attractCenterMaterial;
    [SerializeField] private Material _attractOuterMaterial;
    [SerializeField] private Material _levitateCenterMaterial;
    [SerializeField] private Material _levitateOuterMaterial;


    [Header("Weapon Values")] 
    [SerializeField] private GameObject _magneticObject;

    [SerializeField] private float _pullSpeed;

    public float PullSpeed
    {
        get;
        set;
    }

    [SerializeField] private float _slamSpeed;
    [SerializeField] private float _slamCooldown = 3.0f;
    [SerializeField] private float _shotgunSpeed;
    [SerializeField] private float _shotgunCooldown = 3.0f;
    [SerializeField] private float _reachThreshold = 1.0f;

    [SerializeField] private AudioManager _audioManager;

    [Header("Levitate Ability")] public float _maxFlightDuration = 10;
    [SerializeField] private float _fuelDecrementAmount = 1;
    [SerializeField] private float _fuelRechargeAmount = 1;
    [SerializeField] private floatVariable _flightDuration;
    [SerializeField] private boolVariable _demonInHand;
    [SerializeField] private boolVariable _playerIsFlying;
    [Tooltip("Sets the Y value of player's velocity")] [SerializeField] private float _flightForce = 30f;
    [FormerlySerializedAs("_minimumFuelAmount")] public float _fuelPenaltyThreshold = 1;

    [Header("Player")] [SerializeField] private Rigidbody _playerRigidbody;


    private void Start()
    {
        // caches the reference to the audio manager at start
        _audioManager = FindObjectOfType<AudioManager>();
        
        _flightDuration.Value = _maxFlightDuration;

        Magnetic[] magneticObjects = FindObjectsOfType<Magnetic>();

        foreach (Magnetic magneticObject in magneticObjects)
        {
            _magneticObjects.Add(magneticObject.GetComponent<Rigidbody>());
        }
    }

    private bool _activateMagnet = false;
    
    private void Update()
    {
        if (!_gameIsPaused.Value)
        {

            _activeShotTypeSO.activeShotType = _currentShotType;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _currentShotType = ShotTypeSO.ShotType.Shotgun;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _currentShotType = ShotTypeSO.ShotType.Sniper;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _currentShotType = ShotTypeSO.ShotType.Spray;
            }
           
            if (_flightDuration.Value <= 0f)
            {
                _outOfFuel.Value = true;
            }
            

            // When the player is holding right mouse button and holding space and has demons in hand and has fuel
            if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.Space) && _demonInHand.Value &&
                _flightDuration.Value >= 0)
            {
                // if player is not out of fuel make them fly
                if (!_outOfFuel.Value)
                {
                    transform.position = _footPosition.position;
                    _playerIsFlying.Value = true;
                    _audioManager.StartFlyingSound();
                    _flightDuration.Value -= _fuelDecrementAmount;
                    _attractParticlesRenderer.material = _levitateCenterMaterial;
                    _centerSphere.material = _levitateCenterMaterial;
                    _outerSphere.material = _levitateOuterMaterial;
                    _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, _flightForce, _playerRigidbody.velocity.z);
                }
            }
            else
            {
                if(_playerIsFlying.Value == true) {
                    _audioManager.StopFlyingSound();
                }
                _playerIsFlying.Value = false;
                transform.position = _handPosition.position;
                _attractParticlesRenderer.material = _attractCenterMaterial;
                _centerSphere.material = _attractCenterMaterial;
                _outerSphere.material = _attractOuterMaterial;
                _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y, _playerRigidbody.velocity.z);
            }

            // This makes it so that the player can't just hold the flight ability buttons and have the fuel go from 1 to 0 to 1 over and over again, giving them infinite flight.
            // Makes it so the fuel must regenerate a bit before the player can use flight again 

            if (_flightDuration.Value >= _fuelPenaltyThreshold)
            {
                _outOfFuel.Value = false;
            }

            // if the player is out of fuel stop flying
            if (_flightDuration.Value <= 0)
            {
                _outOfFuel.Value = true;
                transform.position = _handPosition.position;
                _playerIsFlying.Value = false;
                _audioManager.StopFlyingSound();
            }

            // if the player's fuel is not full and the player is not flying, fill up their fuel
            if (_flightDuration.Value < _maxFlightDuration && !_playerIsFlying.Value)
            {
                _flightDuration.Value += _fuelRechargeAmount;
            }

            // TODO: Consider renaming _flightDuration to _flightFuel
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
                _audioManager.PlayPullingEndSound();
                _repelImage.SetActive(false);
                _attractImage.SetActive(false);
                _defaultImage.SetActive(true);
                _attractParticlesRoot.SetActive(false);
            }

            //shotgun push
            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ShotgunAbility());
            }

            //slam 
            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(2))
            {
                StartCoroutine(SlamAbility());
            }
        }

        else
        {
            transform.position = _handPosition.position;
        }
        SetDemonsVelocityAndPosition();
    }
    
    
    private bool _slamOnCooldown;
    private IEnumerator SlamAbility()
    {
        if (!_slamOnCooldown)
        {
            _slamOnCooldown = true;
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

            yield return new WaitForSeconds(_slamCooldown);
            _slamOnCooldown = false;
        }
    }

    private bool _shotgunOnCooldown;
    
    private ShotTypeSO.ShotType _currentShotType;
    
    private IEnumerator ShotgunAbility()
    {
        if (!_shotgunOnCooldown)
        {
            _shotgunOnCooldown = true;

            switch (_currentShotType)
            {
                case ShotTypeSO.ShotType.Shotgun:
                    _audioManager.PlayShotgunSound();
                    break;
                case ShotTypeSO.ShotType.Sniper:
                    _audioManager.PlaySniperSound();
                    break;
                case ShotTypeSO.ShotType.Spray:
                    _audioManager.PlaySpraySound();
                    break;
                case ShotTypeSO.ShotType.Beam:
                    break;
                default:
                    break;
            }

            foreach (Rigidbody rb in _magneticObjects)
            {
                _attractImage.SetActive(false);
                _repelImage.SetActive(true);
                _defaultImage.SetActive(false);
                _attractParticlesRoot.SetActive(false);
                _repelParticlesRoot.SetActive(true);
                _repelParticles.Clear();
                _repelParticles.Play();
                
                //rb.AddExplosionForce(_shotgunSpeed, _handPosition.position, 10f);


                Vector3 offsetDirection;
                float deviationAngleX = 0f;
                float deviationAngleY = 0f;
                float deviationAngleZ = 0f;
                
                switch (_currentShotType)
                {
                    case ShotTypeSO.ShotType.Shotgun:

                        deviationAngleX = Random.Range(-20f, 20f);
                        deviationAngleY = Random.Range(-20f, 20f);
                        deviationAngleZ = Random.Range(-20f, 20f);
                        
                        offsetDirection = Quaternion.Euler(deviationAngleX, deviationAngleY, deviationAngleZ) * transform.forward;
                        
                        Debug.Log("X:" + deviationAngleX + " Y:" + deviationAngleY + " Z:" + deviationAngleZ);
                        
                        rb.AddForce(offsetDirection * _shotgunSpeed);
                        

                        break;
                    case ShotTypeSO.ShotType.Sniper:
                        rb.AddForce(transform.forward * _shotgunSpeed);
                        break;
                    case ShotTypeSO.ShotType.Spray:
                        deviationAngleY = Random.Range(-20f, 20f); // Adjust the range as needed
                        offsetDirection = Quaternion.Euler(0, deviationAngleY, 0) * transform.forward;
                        rb.AddForce(offsetDirection * _shotgunSpeed);
                        break;
                    case ShotTypeSO.ShotType.Beam:
                        break;
                    default:
                        break;
                }

                


                _activateMagnet = false;
            }

            yield return new WaitForSeconds(_shotgunCooldown);
            _shotgunOnCooldown = false;
        }
    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        SetDemonsVelocityAndPosition();
    }


    private void SetDemonsVelocityAndPosition()
    {
        _pullingInDemons.Value = _activateMagnet;

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
                    obj.position = transform.position;
                    _audioManager.PlayObjectReachedSound();
                }
                
            }
        }
    }

    public void AddMagneticCube()
    {
        GameObject magnet = Instantiate(_magneticObject);
        _magneticObjects.Add(magnet.GetComponent<Rigidbody>());
    }

    public void DecreaseDiceWeight(float percentDecrease)
    {
       foreach (Rigidbody rb in _magneticObjects)
       {
           rb.mass -= (rb.mass * percentDecrease);
       } 
    }

    public int GetMagnetCount()
    {
        return _magneticObjects.Count;
    }

    public float GetDiceWeight()
    {
        return _magneticObjects[0].mass;
    }

}