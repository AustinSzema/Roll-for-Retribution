using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    [SerializeField] private Vector3Variable _playerPos;

    [SerializeField] private float _rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {

        // Calculate the direction to the target
        Vector3 direction = _playerPos.Value - transform.position;

        // Rotate towards the target
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

    }
}
