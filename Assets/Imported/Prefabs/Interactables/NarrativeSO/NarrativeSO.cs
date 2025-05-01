using UnityEngine;

[CreateAssetMenu(fileName = "NarrativeSO", menuName = "Scriptable Objects/NarrativeSO")]
public class NarrativeSO : ScriptableObject
{
    public int staminaCost = 1;
    [Multiline]
    public string description = "Enter object description here";



    public string GetObjectInfo()
    {
        return "Click to inspect\nStamina cost: " + staminaCost;
    }

    public bool isRadio = false;
}
