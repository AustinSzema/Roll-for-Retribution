using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn Info Scriptable Object Variable.
/// The gameobject is what were spawning
/// the int variable is the number of them that we spawn every spawn interval 
/// </summary>

[Serializable]
public struct EnemyAndNumber
{
    public GameObject enemy;
    public int number;
}
[Serializable]
public struct SpawnInfo
{
    public int StartTime;
    // GameObject is the thing were spawning
    // Int is the number that we spawn every spawn interval
    public List<EnemyAndNumber> Spawnables;
}

[Serializable]
public struct SpawnInfoList
{
    public SpawnInfo[] spinfo;
}

[CreateAssetMenu(menuName = "Scriptable Object Variable/Spawn Info",
    fileName = "New Spawn Info Variable")]
public class spawnInfoVariable : ScriptableObjectVariable<List<SpawnInfo>>
{
}

