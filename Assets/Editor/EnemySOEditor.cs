using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySO))]
public class EnemySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Reference to the target scriptable object
        EnemySO enemySO = (EnemySO)target;

        // Draw the default fields for the EnemySO
        DrawDefaultInspector();

        switch (enemySO.enemyType)
        {
            case EnemySO.EnemyType.Generic:
                // No additional fields for Generic
                break;
            case EnemySO.EnemyType.ExplodingCharger:
                // Exploding Charger-specific fields
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Exploding Charger Specific Values", EditorStyles.boldLabel);
                enemySO.explosionDamage = EditorGUILayout.FloatField("Explosion Damage", enemySO.explosionDamage);
                enemySO.explosionTimer = EditorGUILayout.FloatField("Explosion Timer", enemySO.explosionTimer);
                enemySO.explosionDiameter = EditorGUILayout.FloatField("Explosion Diameter", enemySO.explosionDiameter);
                enemySO.activationDistance = EditorGUILayout.FloatField("Activation Distance", enemySO.activationDistance);
                enemySO.damagesOtherEnemies = EditorGUILayout.Toggle("Damages Other Enemies", enemySO.damagesOtherEnemies);
                break;
            case EnemySO.EnemyType.Grenadier:
                // Grenadier-specific fields
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Grenadier Specific Values", EditorStyles.boldLabel);
                enemySO.gasDamage = EditorGUILayout.FloatField("Gas Damage", enemySO.gasDamage);
                enemySO.gasDuration = EditorGUILayout.FloatField("Gas Duration", enemySO.gasDuration);
                enemySO.gasCloudDiameter = EditorGUILayout.FloatField("Gas Cloud Diameter", enemySO.gasCloudDiameter);
                enemySO.damageOtherEnemies = EditorGUILayout.Toggle("Damages Other Enemies", enemySO.damageOtherEnemies);
                enemySO.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed", enemySO.projectileSpeed);
                enemySO.projectileCooldown = EditorGUILayout.FloatField("Projectile Cooldown", enemySO.projectileCooldown);
                enemySO.idealDistanceToPlayer = EditorGUILayout.FloatField("Ideal Distance To Player", enemySO.idealDistanceToPlayer);
                enemySO.minDistanceFromPlayer = EditorGUILayout.FloatField("Minimum Distance From Player", enemySO.minDistanceFromPlayer);
                break;
        }
    }
}
