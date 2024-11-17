using System;
using UnityEngine;

public class SetPlayerPosition : MonoBehaviour
{

    private GameManager _gameManager;
    public Rigidbody playerRb;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        _gameManager.playerPosition = transform.position;
        _gameManager.playerRigidBodyVelocity = playerRb.velocity;
        //Debug.Log("player velocity: " + playerRb.velocity);
    }
}
