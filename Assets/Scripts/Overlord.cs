using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.overlordPosition = transform.position;
    }
}
