using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerPOV { FirstPerson, FreeFly, ThirdPerson}

/// <summary>
/// String Scriptable Object Variable.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Object Variable/PlayerPOV",
    fileName = "NewPlayerPOVVariable")]
public class PlayerPOVVariable : ScriptableObjectVariable<PlayerPOV>
{
    
}
