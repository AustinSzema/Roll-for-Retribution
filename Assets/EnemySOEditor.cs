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

        // Only show ExplodingCharger fields if enemyType is ExplodingCharger
        if (enemySO.enemyType == EnemySO.EnemyType.ExplodingCharger)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("ExplodingCharger Specific Values", EditorStyles.boldLabel);
            enemySO.explosionDamage = EditorGUILayout.FloatField("Explosion Damage", enemySO.explosionDamage);
            enemySO.explosionTimer = EditorGUILayout.FloatField("Explosion Timer", enemySO.explosionTimer);
            enemySO.explosionDiameter = EditorGUILayout.FloatField("Explosion Diameter", enemySO.explosionDiameter);
            enemySO.activationDistance = EditorGUILayout.FloatField("Activation Distance", enemySO.activationDistance);
            enemySO.damagesOtherEnemies = EditorGUILayout.Toggle("Damages Other Enemies", enemySO.damagesOtherEnemies);
        }
    }
}