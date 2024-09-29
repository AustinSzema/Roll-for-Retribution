using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Instancer : MonoBehaviour
{
    
    public int Instances;
    public Mesh mesh;
    public Material[] Materials;
    public List<List<Matrix4x4>> Batches = new List<List<Matrix4x4>>();

    private void RenderBatches()
    {
        /*
        foreach (var Batches: List<List<Matrix4x4>> in Batches)
        {
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                Graphics.DrawMeshInstanced(mesh, i, Materials[i], Batch);
            }
        }
        */
    }

    private void Update()
    {
        RenderBatches();
    }

    private void Start()
    {
        int AddedMatrices = 0;
        Batches.Add(new List<Matrix4x4>());

        for (int i = 0; i < Instances; i++)
        {
            if (AddedMatrices < 1000)
            {
                Batches[Batches.Count - 1].Add(item: Matrix4x4.TRS(new Vector3(x: Random.Range(-20, 20), y: Random.Range(-10, 10), z: Random.Range(0, 300)), Random.rotation, new Vector3(x: Random.Range(1, 3), y: Random.Range(1, 3), z: Random.Range(1, 3))));
                AddedMatrices += 1;
            }
            else
            {
                Batches.Add(new List<Matrix4x4>());
                AddedMatrices = 0;
            }
        }
    }
}
