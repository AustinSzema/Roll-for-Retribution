using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private CharacterController _controller;

    [Header("Movement Parameters")]
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _gravityValue = -9.81f;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private Transform _cameraTransform;

    public Vector2 MoveInput { get; set; }

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(MoveInput.x, 0, MoveInput.y);
        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0.0f;
        _controller.Move(move.normalized * Time.deltaTime * _playerSpeed);

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}