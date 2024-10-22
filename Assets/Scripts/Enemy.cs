using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemySO enemySO;
    
    [HideInInspector] public bool dieOnContactWithPlayer = true;
    
    [FormerlySerializedAs("_rigidbody")] [SerializeField] protected Rigidbody rb;
    
    [SerializeField] private Material _enemyPauseMaterial;
    
    [Header("Audio")]
    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _enemyGruntClip;
    [SerializeField] private AudioClip _enemyDamagedClip;

    private float _hitPauseTime = 2f;

    
    private static AudioManager _audioManager;

    private MeshRenderer[] _meshRenderers;



    [Header("Particles")]
    [FormerlySerializedAs("_explosionParticles")] [SerializeField] private ParticleSystem _deathParticles;
    [SerializeField] private ParticleSystem _hitParticles;

    protected bool enemyShouldMove = true;
    private Material[] _originalMaterials;
    private Material[] _pausedMaterials;

    private float _currentHealth;
    private bool _firstDisable = true;

    protected GameManager _gameManager;
    
    private void AddGravity()
    {
        rb.AddForce(Physics.gravity * enemySO._gravityMultiplier, ForceMode.Acceleration);
    }
    
    protected virtual void Start()
    {
        Setup();
    }

    protected void Setup()
    {
        _gameManager = GameManager.Instance;
        _currentHealth = enemySO.healthPoints;
        _audioManager = AudioManager.Instance;

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



    protected virtual void FixedUpdate()
    {
        if (!_gameManager.gameIsPaused && enemyShouldMove)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, _gameManager.playerPosition, enemySO._moveSpeed * Time.deltaTime));
            AddGravity();
        }
    }

    public void takeDamage(float hitPoints)
    {
        _audioManager.PlayHitSound();
        _currentHealth -= hitPoints;
        EnemyHit();
        if (_currentHealth <= 0)
        {
            UnpauseEnemy();
            Die();
            _currentHealth = enemySO.healthPoints;
        }
        else
        {
            _audioManager.PlaySFXAtLocation(_enemyDamagedClip, transform.position);

            if (gameObject.activeSelf)
            {
                StartCoroutine(PauseEnemy(_hitPauseTime));
            }
        }
    }

    private IEnumerator PauseEnemy(float waitTime)
    {
        enemyShouldMove = false;
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
        enemyShouldMove = true;
        SetRendererMaterials(false);
    }

    private void EnemyHit()
    {
        _audioManager.PlaySFXAtLocation(_hitClip, transform.position);
        _hitParticles.Play();
    }

    private void Die()
    {
        _audioManager.PlaySFXAtLocation(_hitClip, transform.position);
        _deathParticles.Play();
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        OnTick();
    }
    
    protected void OnTick()
    {
        if (transform.position.y <= -5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        }

        TryPlayGruntSoundRandomly();
        
        // Get the direction from the enemy to the player
        Vector3 directionToPlayer = _gameManager.playerPosition - transform.position;
        directionToPlayer.y = 0f; // Ignore the y-axis

        // Check if the direction is non-zero before attempting rotation
        if (directionToPlayer != Vector3.zero)
        {
            // Calculate the desired rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Smoothly rotate the enemy towards the player only on the Y axis
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemySO.rotationSpeed * Time.deltaTime);
        }
        
    }

    private void TryPlayGruntSoundRandomly()
    {
        float odds = 0.001f;
        if (Random.value < odds)
        {
            _audioManager.PlaySFXAtLocation(_enemyGruntClip, transform.position);
        }
    }
    
}
