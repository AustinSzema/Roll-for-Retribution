using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandPositionSO : MonoBehaviour
{
    [SerializeField] private Vector3Variable _handPosition;

    // Update is called once per frame
    void Update()
    {
        _handPosition.Value = transform.position;
    }
}
