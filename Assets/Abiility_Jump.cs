using UnityEngine;

[CreateAssetMenu(fileName = "Abiility_Jump", menuName = "Scriptable Objects/Abiility_Jump")]
public class Abiility_Jump : Ability
{
    public override void Activate(PlayerController player)
    {
        player.jumpForce *= 2f;
    }
}
