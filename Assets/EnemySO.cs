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

    // Only show these values if the enemy is an exploding charger
    [Header("ExplodingCharger Specific Values")]
    public float explosionDamage = 25f;
    public float explosionTimer = 3f;
    public float explosionDiameter = 5f;
    public float activationDistance = 15f;
    public bool damagesOtherEnemies = true;

    // Validate and hide/show specific fields based on EnemyType
    private void OnValidate()
    {
        if (enemyType == EnemyType.ExplodingCharger)
        {
            // Values related to ExplodingCharger remain unchanged
        }
        else
        {
            // Reset exploding charger-specific values if not an ExplodingCharger
            explosionDamage = 0;
            explosionTimer = 0;
            explosionDiameter = 0;
            activationDistance = 0;
            damagesOtherEnemies = false;
        }
    }
}