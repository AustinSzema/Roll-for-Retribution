using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialsTransparentWhenPulling : MonoBehaviour
{
    [SerializeField] private boolVariable _pullingInDemons;

    [SerializeField] private MeshRenderer _meshRenderer;
    
    private Material[] _demonMaterials;

    private void Start()
    {
        _demonMaterials = _meshRenderer.materials;
    }

    private void Update()
    {
        if (_pullingInDemons.Value == true)
        {
            foreach (Material mat in _demonMaterials)
            {
                mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, 0f);
            }
        }
        else
        {
            foreach (Material mat in _demonMaterials)
            {
                mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, 1f);
            }
        }
    }
}
