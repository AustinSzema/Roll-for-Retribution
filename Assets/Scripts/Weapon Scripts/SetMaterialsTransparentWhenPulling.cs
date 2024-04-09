using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetMaterialsTransparentWhenPulling : MonoBehaviour
{
    [SerializeField] private boolVariable _pullingInDemons;

    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private TrailRenderer _trailRenderer;

    [SerializeField] private Material _solidTrailMaterial;
    [SerializeField] private Material _transparentTrailMaterial;

    private Material[] _demonMaterials;

    private Material[] _transparentMaterials;
    private Material[] _solidMaterials;
    
    private void Start()
    {
        _demonMaterials = _meshRenderer.materials;
        _transparentMaterials = new[] { new Material(_transparentTrailMaterial) };
        _solidMaterials = new[] { new Material(_solidTrailMaterial) };
    }

    private void Update()
    {
        if (_pullingInDemons.Value)
        {
            foreach (Material mat in _demonMaterials)
            {
                mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, 0f);
            }
            _trailRenderer.materials = _transparentMaterials;
        }
        else
        {
            foreach (Material mat in _demonMaterials)
            {
                mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, 1f);
            }
            _trailRenderer.materials = _solidMaterials;

        }
    }
}
