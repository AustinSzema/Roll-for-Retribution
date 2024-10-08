using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        transform.RotateAround(rotationPoint.position, Vector3.up, rotationSpeed*Time.deltaTime);
    }
}
