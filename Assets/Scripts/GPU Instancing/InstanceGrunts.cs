using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceGrunts : MonoBehaviour
{
    [SerializeField] int size = 10;
    [SerializeField] Mesh mesh;
    [SerializeField] Material material;

    private Matrix4x4[] matrices;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        matrices = new Matrix4x4[size * size * size];

        int i = 0;
            
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    matrices[i] = Matrix4x4.TRS(new Vector3(x * 2, y * 2 + Mathf.Sin(Time.time + x + z), z * 2), Quaternion.identity, Vector3.one);
                    i++;
                }
            }
        }
        Graphics.DrawMeshInstanced(mesh, 0, material, matrices);
     }
}
