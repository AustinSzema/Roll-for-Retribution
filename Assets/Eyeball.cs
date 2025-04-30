using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Eyeball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Quaternion lookRotation = Quaternion.identity;

    [SerializeField] private float rotationSpeed = 200f;
    
    private IEnumerator Start()
    {
        while(true)
      {
          lookRotation = Quaternion.Euler(Random.onUnitSphere * 100f);      
          yield return new WaitForSeconds(Random.Range(0.5f, 2f));
          }
    }

    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
