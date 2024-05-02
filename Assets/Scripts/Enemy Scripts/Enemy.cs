using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

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
    private LODGroup _lodGroup;

    private MeshRenderer _activeMeshRenderer;

    [SerializeField] private Material _enemyPauseMaterial;
    [SerializeField] private float _hitPauseTime = 2f;
    public bool enemyShouldMove = true;
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

        _activeMeshRenderer = GetCurrentLODRenderer();
        _originalMaterials = _activeMeshRenderer.materials;
        _pausedMaterials = new Material[_originalMaterials.Length];

        for (int i = 0; i < _pausedMaterials.Length; i++)
        {
            _pausedMaterials[i] = _enemyPauseMaterial;
        }
    }

    private MeshRenderer GetCurrentLODRenderer()
    {
        LOD[] lods = _lodGroup.GetLODs();

        // Loop through each LOD
        foreach (LOD lod in lods)
        {
            // Check if this LOD is active
            if (lod.screenRelativeTransitionHeight < 1.0f)
            {
                // Get the mesh renderers of the active LOD
                Renderer[] renderers = lod.renderers;

                // Print the name of the first renderer (assuming there's at least one)
                if (renderers.Length > 0)
                {
                    Debug.Log("Active Mesh Renderer: " + renderers[0].gameObject.name);
                    // You can access other properties or methods of the renderer as needed
                    return renderers[0] as MeshRenderer;
                }
            }
        }

        throw new NullReferenceException("No mesh renderer on " + transform.parent.name);
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
                _gameManager.IncreaseSuperMeter();
            }
        }
    }

    private IEnumerator PauseEnemy(float waitTime)
    {
        enemyShouldMove = false;
        _activeMeshRenderer = GetCurrentLODRenderer();
        _activeMeshRenderer.materials = _pausedMaterials;
        Debug.Log("Active mesh renderer " + _activeMeshRenderer.name);
        yield return new WaitForSeconds(waitTime);
        UnpauseEnemy();
        yield return null;
    }

    private void UnpauseEnemy()
    {
        enemyShouldMove = true;
        _activeMeshRenderer = GetCurrentLODRenderer();
        _activeMeshRenderer.materials = _originalMaterials;
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