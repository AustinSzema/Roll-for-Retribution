using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Object Variable/ShotTypeSO",
    fileName = "New ShotTypeSO")]
public class ShotTypeSO : ScriptableObject
{
    public enum ShotType
    {
        Shotgun,
        Sniper,
        Spray,
        Beam
    }

    public ShotType activeShotType;
}
