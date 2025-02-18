using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;

    public abstract void Activate(PlayerController player);
}

