using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerPos : MonoBehaviour
{
    [SerializeField] private Vector3Variable _playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerPos.Value = transform.position;
    }
}
