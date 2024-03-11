using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //Debug.Log("Trail " + _trail.enabled);
    }
}
