using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
public class FollowPlayer : MonoBehaviour
{
    
    [SerializeField] private Vector3Variable _playerPos;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 10f;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private AudioClip _jumpScareSound;
    [SerializeField] private AudioClip[] _hitBabySounds;

    [SerializeField] private AudioSource _audioSource;
    
    private Camera _camera;

    private bool _isMoving = true;

    private BabyJumpScare _babyJumpScare;

    private bool _lostGame = false;

    private CharacterController _playerController;

    private RandomBabySounds _babySounds;

    private PlayerInput _playerInput;
    
    private void Start()
    {
        _camera = Camera.main;
        _babyJumpScare = FindObjectOfType<BabyJumpScare>(true);
        _playerController = FindObjectOfType<CharacterController>();
        _babySounds = FindObjectOfType<RandomBabySounds>();
        _playerInput = FindObjectOfType<PlayerInput>();
    }

    private Vector3 _cameraPosition;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            LoseGame();
        }
    }
    
    private void LoseGame()
    {
        _cameraPosition = _camera.transform.position;

        // Calculate the direction from the camera to the object
        Vector3 direction = transform.position - _cameraPosition;

        RaycastHit hit;
        float maxDistance = direction.magnitude;
        
        if (Physics.Raycast(_cameraPosition, direction, out hit, maxDistance, _layerMask))
        {
            if (hit.collider.gameObject != gameObject || _meshRenderer.isVisible == false)
            {
                // The GameObject is behind a wall or obstacle.
                Debug.Log("GameObject is behind a wall or is not visible to the camera.");
                _babyJumpScare.gameObject.SetActive(true);
                _audioSource.PlayOneShot(_jumpScareSound);
            }
            else
            {
                int index = Random.Range(0, _hitBabySounds.Length);
                _audioSource.PlayOneShot(_hitBabySounds[index]);
            }
        }

        _lostGame = true;
        _playerController.enabled = false;
        _babySounds.gameObject.SetActive(false);

        _playerInput.enabled = false;
        
        StartCoroutine(GoToLoseScreen());
        
        Debug.Log("u died");

    }

    private IEnumerator GoToLoseScreen()
    {

        yield return new WaitUntil(() => _audioSource.isPlaying == false);
        SceneManager.LoadScene("Lose Screen");
    }
    
    private void FixedUpdate()
    {
        _cameraPosition = _camera.transform.position;

        // Calculate the direction from the camera to the object
        Vector3 direction = transform.position - _cameraPosition;

        RaycastHit hit;
        float maxDistance = direction.magnitude;


        if (!_lostGame)
        {
            if (Physics.Raycast(_cameraPosition, direction, out hit, maxDistance, _layerMask))
            {
                if (hit.collider.gameObject != gameObject || _meshRenderer.isVisible == false)
                {
                    // The GameObject is behind a wall or obstacle.
                    Debug.Log("GameObject is behind a wall or is not visible to the camera.");
                    _isMoving = true;
                }
                else
                {
                    _isMoving = false;
                    Debug.Log("GameObject is visible to the camera");

                }
            }
            
        }

        if (_isMoving || _lostGame)
            MoveBaby();



    }

    private void MoveBaby()
    {

        Vector3 direction = _playerPos.Value - transform.position;

        // Project the direction vector onto the XZ plane to get the direction on the Y-axis.
        direction.y = 0;

        // Calculate the rotation to face the target on the Y-axis.
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);


        // Apply the rotation to the rotating object only on the Y-axis.
        _rigidbody.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

        // Move towards the target
        _rigidbody.position += transform.forward * _moveSpeed * Time.fixedDeltaTime;


    }


}
