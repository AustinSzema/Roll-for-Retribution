using System;
using System.Collections;
using UnityEngine;

public class TakesDamage : EnemyComponent, IDamageable
{
    [SerializeField] private GameObject unpausedMesh;
    [SerializeField] private GameObject pausedMesh;

    [SerializeField] private float _hitPauseTime = 2f;

    private RigidbodyConstraints startingConstraints;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _deathParticles;
    [SerializeField] private ParticleSystem _hitParticles;


    [SerializeField] private Rigidbody rb;
    

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

        _hitParticles.transform.parent = null;
        _deathParticles.transform.parent = null;

        EnablePausedMesh(false);
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
        Debug.Log("Current health: " + _currentHealth + ", damage taken: " + hitPoints);
        AudioManager.Instance.PlayHitSound();
        _currentHealth -= hitPoints;
        
        
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
        EnablePausedMesh(true);
        yield return new WaitForSeconds(waitTime);
        UnpauseEnemy();
        yield return null;
    }

    private void EnablePausedMesh(bool pause)
    {
        unpausedMesh.SetActive(!pause);
        pausedMesh.SetActive(pause);
    }

    private void UnpauseEnemy()
    {
        rb.constraints = startingConstraints;
        EnablePausedMesh(false);
    }

    private void EnemyHit()
    {
        _hitParticles.transform.position = transform.position;
        _hitParticles.Play();
    }

    
    public void Die()
    {
        UnpauseEnemy();
        _deathParticles.transform.position = transform.position;
        _deathParticles.Play();
        _currentHealth = enemyBase.enemySO.healthPoints;
        gameObject.SetActive(false);
    }
}

