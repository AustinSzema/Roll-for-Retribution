using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private int count;

    private void Awake()
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(prefabToInstantiate, transform.position + Random.onUnitSphere * prefabToInstantiate.transform.localScale.z, Quaternion.identity);
        }
    }
}
