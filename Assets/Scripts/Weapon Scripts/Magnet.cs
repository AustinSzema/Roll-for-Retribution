using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// TODO: Does this script need to be broken up? It has so many references
public class Magnet : MonoBehaviour
{
    [Header("Magnet Positions")]
    //[SerializeField] private Transform _footPosition;

    [SerializeField] private Transform _handPosition;

    [Header("UI Images")] [SerializeField] private Image _attractImage;
    [SerializeField] private Image _repelImage;
    [SerializeField] private Image _defaultImage;

    [SerializeField] private Sprite _shotgunAttractSprite;
    [SerializeField] private Sprite _shotgunRepelSprite;
    [SerializeField] private Sprite _shotgunDefaultSprite;

    // [SerializeField] private Sprite _rocketAttractSprite;
    // [SerializeField] private Sprite _rocketRepelSprite;
    // [SerializeField] private Sprite _rocketDefaultSprite;


    [Header("Root Objects")] [SerializeField]
    private GameObject _attractParticlesRoot;

    [SerializeField] private GameObject _repelParticlesRoot;
    [SerializeField] private GameObject _gravityParticlesRoot;
    
    [SerializeField] private ParticleSystem _repelParticles;
    [SerializeField] private ParticleSystem _gravityParticles;
    //[SerializeField] private MeshRenderer _centerSphere;
    //[SerializeField] private MeshRenderer _outerSphere;
    // [SerializeField] private Material _attractCenterMaterial;
    // [SerializeField] private Material _attractOuterMaterial;
    // [SerializeField] private Material _levitateCenterMaterial;
    // [SerializeField] private Material _levitateOuterMaterial;




    [SerializeField] private float _slamCooldown = 3.0f;
    [SerializeField] private float _shotgunCooldown = 3.0f;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _outOfBreathClip;

    [Header("Levitate Ability")] public float _maxFlightDuration = 10;
    [SerializeField] private float _fuelDecrementAmount = 1;
    [SerializeField] private float _fuelRechargeAmount = 1;

    [Tooltip("Sets the Y value of player's velocity")] [SerializeField]
    private float _flightForce = 30f;

    [FormerlySerializedAs("_minimumFuelAmount")]
    public float _fuelPenaltyThreshold = 1;

    [Header("Player")] [SerializeField] private Rigidbody _playerRigidbody;


    private GameManager _gameManager;

    
    private void Start()
    {
        _gameManager = GameManager.Instance;

        // caches the reference to the audio manager at start
        _audioManager = AudioManager.Instance;

        _gameManager.flightDuration = _maxFlightDuration;
    }

    private bool _activateMagnet = false;

    private bool _outOfBreathClipPlayed = false;

    private bool _usingShotgun = true;

    private bool canAttractWeapon = true;

    private void Update()
    {
        if (!_gameManager.gameIsPaused)
        {
            // if (Input.GetKeyDown(KeyCode.Q)) // TODO: Make this event based instead of in update
            // {
            //     _usingShotgun = !_usingShotgun;
            //     if (_usingShotgun)
            //     {
            //         _currentShotType = GameManager.ActiveShotType.Shotgun;
            //     }
            //     else
            //     {
            //         _currentShotType = GameManager.ActiveShotType.Rocket;
            //     }
            //
            //     _gameManager.activeShot = _currentShotType;
            // }


            // if (Input.GetKeyDown(KeyCode.Alpha1))
            // {
            //     _currentShotType = GameManager.ActiveShotType.Shotgun;
            // }
            // else if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            //     _currentShotType = GameManager.ActiveShotType.Sniper;
            // }
            // else if (Input.GetKeyDown(KeyCode.Alpha3))
            // {
            //     _currentShotType = GameManager.ActiveShotType.Spray;
            // }

            if (_gameManager.flightDuration <= 0f)
            {
                _gameManager.outOfFuel = true;
            }


            // When the player is holding right mouse button and holding space and has demons in hand and has fuel
            if (Input.GetKey(KeyCode.Space) &&
                _gameManager.flightDuration >= 0)
            {
                // if player is not out of fuel make them fly
                if (!_gameManager.outOfFuel)
                {
                    _outOfBreathClipPlayed = false;
                    _playerRigidbody.AddForce(Vector3.up * 10f);
                    //transform.position = _footPosition.position;
                    _gameManager.playerIsFlying = true;

                    // // Set Sprites
                    // _attractImage.sprite = _rocketAttractSprite;
                    // _repelImage.sprite = _rocketRepelSprite;
                    // _defaultImage.sprite = _rocketDefaultSprite;


                    _audioManager.StartFlyingSound();
                    _gameManager.flightDuration -= _fuelDecrementAmount;
                    // _attractParticlesRenderer.material = _levitateCenterMaterial;
                    // _centerSphere.material = _levitateCenterMaterial;
                    // _outerSphere.material = _levitateOuterMaterial;
                    _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, _flightForce,
                        _playerRigidbody.velocity.z);
                }
                else if (!_outOfBreathClipPlayed)
                {
                    _audioManager.PlayInvariableSFX(_outOfBreathClip);
                    _outOfBreathClipPlayed = true;
                }
            }
            else
            {
                if (_gameManager.playerIsFlying == true)
                {
                    _audioManager.StopFlyingSound();
                }

                _gameManager.playerIsFlying = false;
                transform.position = _handPosition.position;
                // _attractParticlesRenderer.material = _attractCenterMaterial;
                // _centerSphere.material = _attractCenterMaterial;
                // _outerSphere.material = _attractOuterMaterial;
                _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y,
                    _playerRigidbody.velocity.z);
                // Set sprites
                _attractImage.sprite = _shotgunAttractSprite;
                _repelImage.sprite = _shotgunRepelSprite;
                _defaultImage.sprite = _shotgunDefaultSprite;
            }

            // This makes it so that the player can't just hold the flight ability buttons and have the fuel go from 1 to 0 to 1 over and over again, giving them infinite flight.
            // Makes it so the fuel must regenerate a bit before the player can use flight again 

            if (_gameManager.flightDuration >= _fuelPenaltyThreshold)
            {
                _gameManager.outOfFuel = false;
            }

            // if the player is out of fuel stop flying
            if (_gameManager.flightDuration <= 0)
            {
                _gameManager.outOfFuel = true;
                transform.position = _handPosition.position;
                _gameManager.playerIsFlying = false;
                _audioManager.StopFlyingSound();
            }

            // if the player's fuel is not full and the player is not flying, fill up their fuel
            if (_gameManager.flightDuration < _maxFlightDuration && !_gameManager.playerIsFlying)
            {
                _gameManager.flightDuration += _fuelRechargeAmount;
            }

            // TODO: Consider renaming _flightDuration to _flightFuel
            if (Input.GetMouseButton(0) && canAttractWeapon)
            {
                
                _activateMagnet = true;
                _audioManager.StartPullingSound();
                transform.position = _handPosition.position;
                _repelImage.gameObject.SetActive(false);
                _attractImage.gameObject.SetActive(true);
                _defaultImage.gameObject.SetActive(false);
                _attractParticlesRoot.SetActive(true);
 
            }

            if (Input.GetMouseButtonUp(0))
            {
                _activateMagnet = false;
                _audioManager.StopPullingSound();
                _audioManager.PlayPullingEndSound();
                _repelImage.gameObject.SetActive(true);
                _attractImage.gameObject.SetActive(false);
                _defaultImage.gameObject.SetActive(false);
                _attractParticlesRoot.SetActive(false);
                StartCoroutine(ResetHandVisual());
                StartCoroutine(Shoot());
            }


            // //shotgun push
            // if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            // {
            //     StartCoroutine(ShotgunAbility());
            // }

            //slam 
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Slam());
            }

            SetWeaponsVelocityAndPosition();
        }
    }

    private IEnumerator ResetHandVisual()
    {
        canAttractWeapon = false;
        yield return new WaitForSeconds(0.2f);
        _repelImage.gameObject.SetActive(false);
        _attractImage.gameObject.SetActive(false);
        _defaultImage.gameObject.SetActive(true);
        canAttractWeapon = true;

    }

    private bool _slamOnCooldown;

    private IEnumerator Slam()
    {
        if (!_slamOnCooldown)
        {
            _slamOnCooldown = true;
            _audioManager.PlaySlamSound();

            _attractImage.gameObject.SetActive(false);
            _repelImage.gameObject.SetActive(true);
            _defaultImage.gameObject.SetActive(false);
            _attractParticlesRoot.SetActive(false);
            _gravityParticlesRoot.SetActive(true);
            _gravityParticles.Clear();
            _gravityParticles.Play();
            
            foreach (Weapon magnetic in _gameManager.weapons)
            {
                magnetic.Slam();
            }
            
            yield return new WaitForSeconds(_slamCooldown);
            _slamOnCooldown = false;
        }
    }

    private bool _shotgunOnCooldown;

    //private GameManager.ActiveShotType _currentShotType;

    private IEnumerator Shoot()
    {
        if (!_shotgunOnCooldown)
        {
            _shotgunOnCooldown = true;

            _audioManager.PlayShotgunSound();

            // switch (_currentShotType)
            // {
            //     case GameManager.ActiveShotType.Shotgun:
            //         _audioManager.PlayShotgunSound();
            //         break;
            //     case GameManager.ActiveShotType.Rocket:
            //         _audioManager.PlaySniperSound();
            //         break;
            //     case GameManager.ActiveShotType.Spray:
            //         _audioManager.PlaySpraySound();
            //         break;
            //     case GameManager.ActiveShotType.Beam:
            //         break;
            //     default:
            //         break;
            // }

            _attractImage.gameObject.SetActive(false);
            _repelImage.gameObject.SetActive(true);
            _defaultImage.gameObject.SetActive(false);
            _attractParticlesRoot.SetActive(false);
            _repelParticlesRoot.SetActive(true);
            _repelParticles.Clear();
            _repelParticles.Play();
                        
            foreach (Weapon magnetic in _gameManager.weapons)
            {
                Rigidbody rb = magnetic.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
            
            foreach (Weapon magnetic in _gameManager.weapons)
            {
                magnetic.Shoot(transform.forward);
            }
            
                
            _activateMagnet = false;

            yield return new WaitForSeconds(_shotgunCooldown);
            _shotgunOnCooldown = false;
        }
    }

    private void FixedUpdate()
    {
        SetWeaponsVelocityAndPosition();
    }

    private void SetWeaponsVelocityAndPosition()
    {
        _gameManager.pullingInDemons = _activateMagnet;

        if (_activateMagnet)
        {
            foreach (Weapon magnetic in _gameManager.weapons)
            {
                Rigidbody rb = magnetic.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Distance threshold to decide when to snap
                    float snapDistance = 3f; // Adjust as needed
                    float distance = Vector3.Distance(magnetic.transform.position, transform.position);

                    if (distance <= snapDistance)
                    {
                        // Snap directly to the hand position if within snapping range
                        rb.isKinematic = true;
                        magnetic.transform.position = transform.position;
                    }
                    else
                    {
                        // Otherwise, attract toward the hand position
                        rb.isKinematic = false;
                        magnetic.Attract(transform.position); // Use the Attract method for gradual movement
                    }
                }
            }
        }
        else
        {
            // Reset the objects to non-kinematic when not magnetized
            foreach (Weapon magnetic in _gameManager.weapons)
            {
                Rigidbody rb = magnetic.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
        }
    }


}