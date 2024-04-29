using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int healthPoints = 1;

    [SerializeField] private GameObject _enemy;

    [SerializeField] private ParticleSystem _explosionParticles;

    [SerializeField] private ParticleSystem _hitParticles;

    [SerializeField] private AudioClip _hitClip;

    [SerializeField] private AudioClip _enemyGruntClip;

    [SerializeField] private AudioClip _enemyDamagedClip;
    
    private static AudioManager _audioManager;

    [Header("Enemy Hit Values")] [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField] private Material _enemyPauseMaterial;
    [SerializeField] private float _hitPauseTime = 2f;
    public bool enemyShouldMove = true;
    private Material[] _originalMaterials;
    private Material[] _pausedMaterials;

    private int _currentHealth;

    private bool _firstDisable = true;

    private GameManager _gameManager;
    
    private void Awake()
    {
        // this needs to be refactored when proper health system is in place
        // health should not be hard coded in awake
        _currentHealth = healthPoints;
        _audioManager = FindObjectOfType<AudioManager>();

        if (_hitClip == null)
        {
            _hitClip = Resources.Load<AudioClip>("Audio/Hit");
        }

        if (_enemyGruntClip == null)
        {
            _enemyGruntClip = Resources.Load<AudioClip>("Audio/EnemyGrunt");
        }

        if (_enemyDamagedClip == null)
        {
            _enemyDamagedClip = Resources.Load<AudioClip>("Audio/SummonerHit");
        }
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _originalMaterials = _meshRenderer.materials;
        _pausedMaterials = new Material[_originalMaterials.Length];

        for (int i = 0; i < _pausedMaterials.Length; i++)
        {
            _pausedMaterials[i] = _enemyPauseMaterial;
        }
    }

    private void OnDisable()
    {
        if (_firstDisable)
        {
            _firstDisable = false;
        }
        else
        {
            SpawnEnemies.EnemiesInScene--;
            _gameManager.killCount++;
        }
    }

    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private Vector3Variable _playerPosition;

    [SerializeField] private boolVariable _gameIsPaused;

    [SerializeField] private Rigidbody _rigidbody;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_gameIsPaused.Value == false && enemyShouldMove)
        {
            _rigidbody.MovePosition(Vector3.MoveTowards(transform.position, _playerPosition.Value,
                _moveSpeed * Time.deltaTime));
        }
    }

    public void takeDamage(int hitPoints)
    {
        _currentHealth -= hitPoints;
        EnemyHit();
        if (_currentHealth <= 0)
        {
            UnpauseEnemy();
            Explode();
            _currentHealth = healthPoints;
        }
        else
        {
            _audioManager.PlaySFXAtLocation(_enemyDamagedClip, transform.position);

            if (gameObject.activeSelf)
            {
                StartCoroutine(PauseEnemy(_hitPauseTime));
                _gameManager.IncreaseSuperMeter();
            }
        }
    }

    private IEnumerator PauseEnemy(float waitTime)
    {
        enemyShouldMove = false;
        _meshRenderer.materials = _pausedMaterials;
        yield return new WaitForSeconds(waitTime);
        UnpauseEnemy();
        yield return null;
    }

    private void UnpauseEnemy()
    {
        enemyShouldMove = true;
        _meshRenderer.materials = _originalMaterials;
    }

    private void EnemyHit()
    {
        if (_hitClip != null)
        {
            _audioManager.PlaySFXAtLocation(_hitClip, transform.position);
        }
        else
        {
            Debug.LogWarning("Hit clip is null in " + gameObject.name);
        }

        _hitParticles.Play();
    }

    private void Explode()
    {
        if (_hitClip != null)
        {
            _audioManager.PlaySFXAtLocation(_hitClip, transform.position);
        }
        else
        {
            Debug.LogWarning("Hit clip is null in " + gameObject.name);
        }

        _explosionParticles.Play();
        _enemy.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.y <= -5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        }

        TryPlayGruntSoundRandomly();
    }

    private void TryPlayGruntSoundRandomly()
    {
        float odds = 0.001f;
        if (UnityEngine.Random.value < odds)
        {
            _audioManager.PlaySFXAtLocation(_enemyGruntClip, transform.position);
        }
    }
}