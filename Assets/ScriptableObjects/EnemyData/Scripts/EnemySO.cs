using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySO", menuName = "EnemyStuff/EnemySO")]
public class EnemySO : ScriptableObject
{
    public enum EnemyType
    {
        Generic,
        ExplodingCharger,
        Grenadier
    }

    public EnemyType enemyType;

    public float healthPoints = 1;
    public float _moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float _gravityMultiplier = 2f;
    public int killThresholdValue = 1;
    public bool isFlyingType = false;
    public bool dieOnContactWithPlayer = true;

    // Only show these values if the enemy is an ExplodingCharger
    [Header("ExplodingCharger Specific Values")]
    [HideInInspector] public float explosionDamage = 25f;
    [HideInInspector] public float explosionTimer = 3f;
    [HideInInspector] public float explosionDiameter = 5f;
    [HideInInspector] public float activationDistance = 15f;
    [HideInInspector] public bool damagesOtherEnemies = true;
    

    
    [Header("Grenadier Specific Values")]
    [HideInInspector] public float gasDamage = 5f;
    [HideInInspector] public float gasDuration = 10f;
    [HideInInspector] public float gasCloudDiameter = 15f;
    [HideInInspector] public bool damageOtherEnemies = true;
    [HideInInspector] public float projectileSpeed = 50f;
    [HideInInspector] public float projectileCooldown = 10f;
    [HideInInspector] public float idealDistanceToPlayer = 100f;
    [HideInInspector] public float minDistanceFromPlayer = 10f;

    
}