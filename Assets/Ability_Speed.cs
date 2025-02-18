using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Speed", menuName = "Scriptable Objects/Ability")]
public class Ability_Speed : Ability
{
    public float speedMultiplier = 2f;
    public override void Activate(PlayerController player)
    {
        player.moveSpeed *= speedMultiplier;
    }
}
