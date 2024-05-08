using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public int healthPoints = 1;

    [SerializeField] private GameObject _enemy;

    [SerializeField] private ParticleSystem _explosionParticles;

    [SerializeField] private ParticleSystem _hitParticles;

    [SerializeField] private AudioClip _hitClip;

    [SerializeField] private AudioClip _enemyGruntClip;

    [SerializeField] private AudioClip _enemyDamagedClip;

    private static AudioManager _audioManager;

    [Header("Enemy Hit Values")]
    
    private MeshRenderer _activeMeshRenderer;

    private MeshRenderer[] _meshRenderers;

    [SerializeField] private Material _enemyPauseMaterial;
    [SerializeField] private float _hitPauseTime = 2f;

    private bool enemyShouldMove = true;
    private Material[] _originalMaterials;
    private Material[] _pausedMaterials;

    private int _currentHealth;

    private bool _firstDisable = true;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        _currentHealth = healthPoints;
        _audioManager = AudioManager.Instance;

        _meshRenderers = GetComponentInChildren<LODGroup>().GetComponentsInChildren<MeshRenderer>(); // not performant but makes editing way easier
        
        _activeMeshRenderer = _meshRenderers[0];
        _originalMaterials = _activeMeshRenderer.materials;
        _pausedMaterials = new Material[_originalMaterials.Length];

        for (int i = 0; i < _pausedMaterials.Length; i++)
        {
            _pausedMaterials[i] = _enemyPauseMaterial;
        }

        enemyShouldMove = _gameManager.enemiesShouldMove;
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


    [SerializeField] private Rigidbody _rigidbody;


    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_gameManager.gameIsPaused == false && enemyShouldMove)
        {
            _rigidbody.MovePosition(Vector3.MoveTowards(transform.position, _gameManager.playerPosition,
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
            }
        }
        _gameManager.IncreaseSuperMeter();
    }

    private IEnumerator PauseEnemy(float waitTime)
    {
        enemyShouldMove = false;

        
        _activeMeshRenderer.materials = _pausedMaterials;
        SetRendererMaterials(true);
        Debug.Log("Active mesh renderer " + _activeMeshRenderer.name);
        yield return new WaitForSeconds(waitTime);
        UnpauseEnemy();
        yield return null;
    }

    private void SetRendererMaterials(bool pause)
    {

        foreach (MeshRenderer mr in _meshRenderers)
        {
            if (pause)
            {
                mr.materials = _pausedMaterials;
            }
            else
            {
                mr.materials = _originalMaterials;
            }
            
        }
    }

    private void UnpauseEnemy()
    {
        enemyShouldMove = true;
        _activeMeshRenderer.materials = _originalMaterials;
        SetRendererMaterials(false);
    }

    private void EnemyHit()
    {
        _audioManager.PlaySFXAtLocation(_hitClip, transform.position);

        _hitParticles.Play();
    }

    private void Explode()
    {
        _audioManager.PlaySFXAtLocation(_hitClip, transform.position);

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