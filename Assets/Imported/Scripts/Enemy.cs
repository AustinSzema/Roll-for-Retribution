using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IInteractable
{
    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Attack Settings")]
    public int damage = 1;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    [Header("Movement Settings")]
    public float speed = 3f;
    public Transform player;
    private NavMeshAgent agent;
    private bool canMove = true;

    [Header("Visual Settings")]
    [SerializeField] private Material enemyFrozenMaterial;
    private List<MeshRenderer> enemyMeshRenderers = new List<MeshRenderer>();
    private List<Material> enemyStartingMaterials = new List<Material>();

    [Header("Effects & Components")]
    [SerializeField] private ParticleSystem bloodParticles;
    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource enemyHitSound;
    private enum EnemyState
    {
        Idle,
        Moving
    }

    private EnemyState enemyState = EnemyState.Idle;
    public event Action<GameObject> OnEnemyDeath;

    private bool isBeingRendered = true;

    private void Start()
    {
        currentHealth = maxHealth;
        player = PlayerHealth.instance?.transform;

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = speed;
        }

        enemyMeshRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
        enemyStartingMaterials = enemyMeshRenderers.Select(mr => mr.material).ToList();

        enemyState = EnemyState.Moving;
    }

    private void Update()
    {
        int visibleCount = 0;
        for (int i = 0; i < enemyMeshRenderers.Count; i++)
        {
            if (enemyMeshRenderers[i].isVisible)
            {
                visibleCount++;
            }
        }

        isBeingRendered = visibleCount > 0;

        if (isBeingRendered && canMove && player)
        {
            enemyState = EnemyState.Moving;

            Vector3 lookAtPosition = player.position;
            lookAtPosition.y = transform.position.y;
            transform.LookAt(lookAtPosition);

            if (agent != null && agent.isActiveAndEnabled)
            {
                agent.SetDestination(player.position);
            }
        }

        switch (enemyState)
        {
            case EnemyState.Idle:
                animator.SetBool("IsMoving", false);
                break;
            case EnemyState.Moving:
                animator.SetBool("IsMoving", true);
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(damage);
            TakeDamage(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHitSound.PlayOneShot(enemyHitSound.clip);
        currentHealth -= damage;
        bloodParticles.Play();
        FreezeEnemy();

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        bloodParticles.transform.parent = null;
        bloodParticles.transform.localScale = Vector3.one;
        var main = bloodParticles.main;
        main.stopAction = ParticleSystemStopAction.Destroy;

        OnEnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }

    public void Interact()
    {
        
    }

    private void FreezeEnemy()
    {
        StartCoroutine(StopMoving());
    }

    private IEnumerator StopMoving()
    {
        SetMaterialsToFrozen();
        canMove = false;

        if (agent != null)
        {
            agent.isStopped = true;
        }

        yield return new WaitForSeconds(0.5f);

        canMove = true;

        if (agent != null)
        {
            agent.isStopped = false;
        }

        ResetMaterials();
    }

    private void SetMaterialsToFrozen()
    {
        foreach (var mr in enemyMeshRenderers)
            mr.material = enemyFrozenMaterial;
    }

    private void ResetMaterials()
    {
        for (int i = 0; i < enemyMeshRenderers.Count; i++)
            enemyMeshRenderers[i].material = enemyStartingMaterials[i];
    }
}
