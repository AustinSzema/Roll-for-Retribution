using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;
using UnityEngine.AI;

public class WalkAndPlayGuitarSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Transform _actorTransform;
    [SerializeField] private NavMeshAgent _actorNavMeshAgent;
    [SerializeField] private Animator _actorAnimator;
    [SerializeField] private GameObject _guitarObject;
    [SerializeField] private AudioSource _guitarPlayAudio;
    [SerializeField] private CanvasGroupFader _sceneFader;
    [SerializeField] private ApplicationManager _appManager;

    [Header("Navigation Parameters")]
    [SerializeField] private Transform[] _path;

    [Header("Rotation Parameters")]
    [SerializeField] private Transform _lookAtBeforeSit;
    [SerializeField] private float _rotationSpeed = 120.0f;
    [SerializeField] private float _rotationTimeout = 2.0f;

    [Header("Sequence Parameters")]
    [SerializeField] private float _playGuitarForDuration = 5.0f;
    [SerializeField] private int _sceneToLoad = 2;

    private bool _actorIsMoving;
    private int _currentPathNodeIndex;

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
                _actorAnimator.SetTrigger("StopWalking");
                _actorIsMoving = false;
                StartCoroutine(RotateTowardsTarget());
            }
        }
    }

    public void Begin()
    {
        _actorIsMoving = true;
        _actorAnimator.SetTrigger("StartWalking");
        _actorNavMeshAgent.destination = _path[_currentPathNodeIndex].position;
    }

    private bool HasReachedDestination()
    {
        return !_actorNavMeshAgent.pathPending &&
            _actorIsMoving &&
            _actorNavMeshAgent.remainingDistance <=
            _actorNavMeshAgent.stoppingDistance + 0.1f;
    }

    private IEnumerator PlayGuitarRoutine()
    {
        yield return new WaitForSeconds(_playGuitarForDuration);
        yield return _sceneFader.FadeRoutine(true);
        _appManager.LoadSceneSingle(_sceneToLoad);
        Debug.Log("Switch to new scene!");
    }

    private IEnumerator RotateTowardsTarget()
    {
        Vector3 direction =
            (_lookAtBeforeSit.position - _actorTransform.position).normalized;
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

        _actorAnimator.SetTrigger("PlayGuitar");
        _guitarObject.SetActive(true);
        _guitarPlayAudio.Play();
        StartCoroutine(PlayGuitarRoutine());
    }
}
