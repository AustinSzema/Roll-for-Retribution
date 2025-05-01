using UnityEditor;
using UnityEngine;

public static class RemoveMissingScripts
{
    [MenuItem("Tools/Remove Missing Scripts in Scene")]
    static void RemoveAllMissingScriptsInScene()
    {
        int removedCount = 0;
        var gameObjects = Object.FindObjectsOfType<GameObject>(true); // true includes inactive

        foreach (var go in gameObjects)
        {
            int countBefore = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
            if (countBefore > 0)
            {
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                removedCount += countBefore;
                Debug.Log($"Removed {countBefore} missing scripts from '{go.name}'", go);
            }
        }

        Debug.Log($"✅ Finished removing missing scripts. Total removed: {removedCount}");
    }
}
