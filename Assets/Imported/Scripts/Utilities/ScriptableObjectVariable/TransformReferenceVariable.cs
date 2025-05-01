using UnityEngine;

/// <summary>
/// Transform Scriptable Object Variable. 
/// 
/// Note: Transforms cannot truly be stored at the asset level. Instead, Unity 
/// stores a reference to the transform at the asset level, preserving the
/// ability of the transform to be passed around in-scene using this variable.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Object Variable/Transform Reference",
    fileName = "NewTransformReferenceVariable")]
public class TransformReferenceVariable : ScriptableObjectVariable<Transform>
{

}
