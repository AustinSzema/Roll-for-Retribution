using UnityEngine;

[CreateAssetMenu(menuName = "MagnetState", fileName = "NewMagnetState")]
public class MagnetState : ScriptableObject
{
    public enum magnetState {Attracting, Repelling}
}
