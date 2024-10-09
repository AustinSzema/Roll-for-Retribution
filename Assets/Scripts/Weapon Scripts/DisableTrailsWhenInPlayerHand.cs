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

    
    // TODO: This code probably needs a refactor or does not need to be its own script
    // Update is called once per frame
    void Update()
    {

        _trail.enabled = Vector3.Distance(transform.position, _gameManager.handPosition) > 10f;


        
    }

}
