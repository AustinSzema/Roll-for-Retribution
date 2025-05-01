using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class End : MonoBehaviour
{
    [SerializeField] private float maxXPos;
    [SerializeField] private float maxZPos;
    [SerializeField] private float minXPos;
    [SerializeField] private float minZPos;
  
    // Start is called before the first frame update
    void Start()
    {
        changePos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
      changePos();
    }

    private void changePos()
    {
      Vector3 newPos = new Vector3(Random.Range(minXPos, maxXPos), -10, Random.Range(minZPos, maxZPos));
      this.gameObject.transform.position = newPos;
    }
}
