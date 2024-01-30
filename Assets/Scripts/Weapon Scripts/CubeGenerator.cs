using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    [SerializeField] private int _numberOfCubes = 100;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < _numberOfCubes; i++)
        {
            Instantiate(_cubePrefab, transform.position, quaternion.identity);
        }
    }
}
