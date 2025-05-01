using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMeshLetter : MonoBehaviour
{


    [SerializeField] private Mesh[] Letters;

    [SerializeField] private MeshFilter projectileMeshFilter;

    // Start is called before the first frame update
    void Start()
    {
        projectileMeshFilter.mesh = Letters[Random.Range(0, Letters.Length - 1)];
        
    }

}
