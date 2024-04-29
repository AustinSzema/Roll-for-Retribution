using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTrailsWhenInPlayerHand : MonoBehaviour
{

    [SerializeField] private TrailRenderer _trail;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Vector3.Distance(transform.position, _gameManager.handPosition) <= 1f)
        {
            _trail.enabled = false;
        }
        else
        {
            _trail.enabled = true;
        }

        
        
        float distance = Mathf.Clamp(Vector3.Distance(transform.position, _gameManager.handPosition), 0.5f, 1f);
        transform.localScale = new Vector3(distance, distance, distance);

        //Debug.Log("Trail " + _trail.enabled);
    }

    private void LateUpdate()
    {
        

        if (Input.GetMouseButton(0) && Vector3.Distance(transform.position, _gameManager.handPosition) <= 3f)
        {
            transform.position = _gameManager.handPosition;
        }
    }
}
