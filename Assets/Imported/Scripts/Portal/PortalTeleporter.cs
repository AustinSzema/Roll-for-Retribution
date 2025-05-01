using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _receiver;

    private bool _playerIsOverlapping = false;

    private void LateUpdate()
    {
        if (_playerIsOverlapping)
        {
            
            Vector3 portalToPlayer = _player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0.0f)
            {
                float rotationDifference = -Quaternion.Angle(transform.rotation, _receiver.rotation);
                rotationDifference += 180;
                _player.Rotate(Vector3.up, rotationDifference);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * portalToPlayer;
                Debug.Log(positionOffset);
                _controller.enabled = false;
                _player.position = _receiver.position + (positionOffset);
                _controller.enabled = true;

                _playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsOverlapping = false;
        }
    }
}
