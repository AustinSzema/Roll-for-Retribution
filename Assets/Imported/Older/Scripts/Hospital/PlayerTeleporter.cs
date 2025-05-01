using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private CharacterController _playerCharacterController;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _hallwayTeleportDestination;
    [SerializeField] private Transform _room4TeleportDestination;

    public void TeleportToRoomFour()
    {
        _playerCharacterController.enabled = false;
        _playerTransform.position = _room4TeleportDestination.position;
        _playerCharacterController.enabled = true;
    }
    public void TeleportToHallway()
    {
        _playerCharacterController.enabled = false;
        _playerTransform.position = _hallwayTeleportDestination.position;
        _playerCharacterController.enabled = true;
    }
}
