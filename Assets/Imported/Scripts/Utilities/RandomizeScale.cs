using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeScale : MonoBehaviour
{
    public float scaleMin;
    public float scaleMax;
    public float randomRadiusRange;

    private void Start()
    {
        Quaternion randomRotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
        
        Vector3 randomSpherePos = Random.insideUnitSphere * randomRadiusRange;

        transform.localPosition = new Vector3(randomSpherePos.x, 0.0f, randomSpherePos.z);
        transform.localRotation = randomRotation;
        transform.localScale =
            Vector3.one * Random.Range(scaleMin, scaleMax);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.parent.position, randomRadiusRange);
    }
}
