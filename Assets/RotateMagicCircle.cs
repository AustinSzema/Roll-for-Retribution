using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMagicCircle : MonoBehaviour
{
    public enum RotationAxis
    {
        xAxis,
        yAxis,
        zAxis,
        negXAxis,
        negYAxis,
        negZAxis
    };

    [SerializeField] private RotationAxis _rotationAxis;

    private float xAngle = 0f;
    private float yAngle = 0f;
    private float zAngle = 0f;

    private float _rotationSpeed = 100f;
    
    void Update()
    {
        switch (_rotationAxis)
        {
            case RotationAxis.xAxis:
                xAngle = Time.deltaTime * _rotationSpeed;
                break;
            case RotationAxis.yAxis:
                yAngle = Time.deltaTime * _rotationSpeed;
                break;
            case RotationAxis.zAxis:
                zAngle = Time.deltaTime * _rotationSpeed;
                break;
            case RotationAxis.negXAxis:
                xAngle = Time.deltaTime * -_rotationSpeed;
                break;
            case RotationAxis.negYAxis:
                yAngle = Time.deltaTime * -_rotationSpeed;
                break;
            case RotationAxis.negZAxis:
                zAngle = Time.deltaTime * -_rotationSpeed;
                break;
        }
        transform.Rotate(xAngle, yAngle, zAngle, 0f);
    }
}
