using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTrailsWhenInPlayerHand : MonoBehaviour
{
    [SerializeField] private Vector3Variable _handPosition;

    [SerializeField] private TrailRenderer _trail;

    // Update is called once per frame
    void Update()
    {

        
        
        if (Vector3.Distance(transform.position, _handPosition.Value) <= 1f)
        {
            _trail.enabled = false;
        }
        else
        {
            _trail.enabled = true;
        }

        
        
        float distance = Mathf.Clamp(Vector3.Distance(transform.position, _handPosition.Value), 0.5f, 1f);
        transform.localScale = new Vector3(distance, distance, distance);

        //Debug.Log("Trail " + _trail.enabled);
    }

    private void LateUpdate()
    {
        

        if (Vector3.Distance(transform.position, _handPosition.Value) <= 3f)
        {
            transform.position = _handPosition.Value;
        }
    }
}
