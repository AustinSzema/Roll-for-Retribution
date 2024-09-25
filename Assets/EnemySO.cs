using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySO", menuName = "Scriptable Object Variable/EnemySO")]
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
    public bool isFlyingType = false;
    public bool dieOnContactWithPlayer = true;

    // Only show these values if the enemy is an ExplodingCharger
    [Header("ExplodingCharger Specific Values")]
    [HideInInspector] public float explosionDamage = 25f;
    [HideInInspector] public float explosionTimer = 3f;
    [HideInInspector] public float explosionDiameter = 5f;
    [HideInInspector] public float activationDistance = 15f;
    [HideInInspector] public bool damagesOtherEnemies = true;

    // Validate and hide/show specific fields based on EnemyType
    private void OnValidate()
    {
        if (enemyType == EnemyType.ExplodingCharger)
        {
            // Values related to ExplodingCharger are kept as they are
        }
        else
        {
            // Reset ExplodingCharger-specific values if not an ExplodingCharger
            explosionDamage = 0;
            explosionTimer = 0;
            explosionDiameter = 0;
            activationDistance = 0;
            damagesOtherEnemies = false;
        }
    }
}