using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TakesDamage : EnemyComponent, IDamageable
{
    [SerializeField] private Material _enemyPauseMaterial;

    private float _hitPauseTime = 2f;
    
    private MeshRenderer[] _meshRenderers;

    private RigidbodyConstraints startingConstraints;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _deathParticles;
    [SerializeField] private ParticleSystem _hitParticles;


    [SerializeField] private Rigidbody rb;
    
    private Material[] _originalMaterials;
    private Material[] _pausedMaterials;

    private float _currentHealth;
    private bool _firstDisable = true;

    
    private GameManager _gameManager;
    
    protected virtual void Start()
    {
        Setup();
    }

    protected void Setup()
    {
        _gameManager = GameManager.Instance;
        _currentHealth = enemyBase.enemySO.healthPoints;


        startingConstraints = rb.constraints;
        
        // Get all MeshRenderers in children without using LODGroup
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();

        
        // If there are no mesh renderers, log a warning
        if (_meshRenderers.Length == 0)
        {
            Debug.LogWarning("No MeshRenderers found in child objects.");
            return;
        }

        // Assume all mesh renderers share the same material setup
        _originalMaterials = _meshRenderers[0].materials;
        _pausedMaterials = new Material[_originalMaterials.Length];

        for (int i = 0; i < _pausedMaterials.Length; i++)
        {
            _pausedMaterials[i] = _enemyPauseMaterial;
        }

        _hitParticles.transform.parent = null;
        _deathParticles.transform.parent = null;

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

    

    public void takeDamage(float hitPoints)
    {
        if (enemyBase.enemySO.dieOnContactWithPlayer)
        {
            Die();
        }
        else
        {
            _currentHealth -= hitPoints;
        }
        
        EnemyHit();
        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(PauseEnemy(_hitPauseTime));
            }
        }
    }

    private IEnumerator PauseEnemy(float waitTime)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        SetRendererMaterials(true);
        yield return new WaitForSeconds(waitTime);
        UnpauseEnemy();
        yield return null;
    }

    private void SetRendererMaterials(bool pause)
    {
        foreach (MeshRenderer mr in _meshRenderers)
        {
            mr.materials = pause ? _pausedMaterials : _originalMaterials;
        }
    }

    private void UnpauseEnemy()
    {
        rb.constraints = startingConstraints;
        SetRendererMaterials(false);
    }

    private void EnemyHit()
    {
        _hitParticles.transform.position = transform.position;
        _hitParticles.Play();
    }

    
    private void Die()
    {
        UnpauseEnemy();
        _deathParticles.transform.position = transform.position;
        _deathParticles.Play();
        gameObject.SetActive(false);
        _currentHealth = enemyBase.enemySO.healthPoints;
    }
}

