using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class OpenDoorAndWalkSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Transform _actorTransform;
    [SerializeField] private NavMeshAgent _actorNavMeshAgent;
    [SerializeField] private Animator _actorAnimator;
    [SerializeField] private Door _doorToOpen;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _doorKnockClip;

    [Header("Timing Parameters")]
    [SerializeField] private float _waitForKnockTime;

    [Header("Animation Parameters")]
    [SerializeField] private bool _isHoldingObject;
    [SerializeField] private float _waitForObjectPlaceDuration = 2.25f;
    [SerializeField] private float _waitForIdleTransitionDuration = 2.25f;
    [SerializeField] private float _placementAngle = 30.0f;
    [SerializeField] private Transform _objectSlot;
    [SerializeField] private Transform _placedObjectSlot;
    [SerializeField] private GameObject _objectPrefab;

    [Header("Navigation Parameters")]
    [SerializeField] private Transform[] _path;

    [Header("Rotation Parameters")]
    [SerializeField] private Transform _lookAtWhenDone;
    [SerializeField] private float _rotationSpeed = 2.0f;
    [SerializeField] private float _rotationTimeout = 2.0f;

    [Header("Events")]
    [SerializeField] private UnityEvent _onDestinationReached;

    private bool _actorIsMoving;
    private int _currentPathNodeIndex;
    private GameObject _carriedObject;

    private void OnEnable()
    {
        _doorToOpen.AnimationComplete += WalkThroughDoor;
    }
    private void OnDisable()
    {
        _doorToOpen.AnimationComplete -= WalkThroughDoor;
    }

    void Update()
    {
        if (HasReachedDestination())
        {
            if (_currentPathNodeIndex + 1 < _path.Length)
            {
                _currentPathNodeIndex++;
                _actorNavMeshAgent.destination =
                    _path[_currentPathNodeIndex].position;
            }
            else
            {
                if (_isHoldingObject)
                {
                    _actorAnimator.SetTrigger("StopWalkingWithObject");
                    _actorAnimator.SetTrigger("PlaceObject");
                    StartCoroutine(WaitForObjectPlaceRoutine());
                }
                else
                {
                    _actorAnimator.SetTrigger("StopWalking");
                }
                _actorIsMoving = false;
                StartCoroutine(RotateTowardsTarget());
            }
        }
    }

    public void Begin()
    {
        StartCoroutine(BeginRoutine());
    }

    private IEnumerator BeginRoutine()
    {
        if (_isHoldingObject)
        {
            _actorAnimator.SetTrigger("CarryObject");
            _carriedObject = Instantiate(_objectPrefab, _objectSlot);
        }

        if (_audioSource != null)
        {
            _audioSource.PlayOneShot(_doorKnockClip);
        }
        
        yield return new WaitForSeconds(_waitForKnockTime);
        _doorToOpen.Open();
    }

    public void WalkThroughDoor()
    {
        _actorIsMoving = true;
        if (_isHoldingObject)
        {
            _actorAnimator.SetTrigger("StartWalkingWithObject");
        }
        else
        {
            _actorAnimator.SetTrigger("StartWalking");
        }
        _actorNavMeshAgent.destination = _path[_currentPathNodeIndex].position;
    }

    private bool HasReachedDestination()
    {
        return !_actorNavMeshAgent.pathPending &&
            _actorIsMoving &&
            _actorNavMeshAgent.remainingDistance <=
            _actorNavMeshAgent.stoppingDistance + 0.1f;
    }

    private IEnumerator RotateTowardsTarget()
    {
        Vector3 direction =
            (_lookAtWhenDone.position - _actorTransform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float elapsedTime = 0.0f;

        while (Quaternion.Angle(_actorTransform.rotation, targetRotation) > 1.5f
            && elapsedTime < _rotationTimeout)
        {
            _actorTransform.rotation =
                Quaternion.RotateTowards(_actorTransform.rotation,
                targetRotation, Time.deltaTime * _rotationSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (!_isHoldingObject)
        {
            _onDestinationReached?.Invoke();
        }
    }

    private IEnumerator WaitForObjectPlaceRoutine()
    {
        yield return new WaitForSeconds(_waitForObjectPlaceDuration);
        _actorAnimator.SetTrigger("StartIdle");
        _carriedObject.transform.parent = _placedObjectSlot;
        _carriedObject.transform.localRotation =
            Quaternion.Euler(0.0f, _placementAngle, 0.0f);
        _carriedObject.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(_waitForIdleTransitionDuration);
        _onDestinationReached?.Invoke();
    }
}