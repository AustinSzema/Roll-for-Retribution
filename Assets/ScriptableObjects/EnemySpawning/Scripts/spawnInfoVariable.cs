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

    public int MaxEnemiesInScene;
}


[CreateAssetMenu(menuName = "EnemyStuff/Spawn Info",
    fileName = "New Spawn Info Variable")]
public class spawnInfoVariable : ScriptableObjectVariable<List<SpawnInfo>>
{
}

