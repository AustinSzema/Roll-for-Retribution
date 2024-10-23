using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetMaterialsTransparentWhenPulling : MonoBehaviour
{

    [Header("Trail")]
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private Material _solidTrailMaterial;
    [SerializeField] private Material _transparentTrailMaterial;

    [Header("Attract Demon")]
    [SerializeField] private GameObject _attractDemonVisual;
    [SerializeField] private MeshRenderer _attractMeshRenderer;
    private Material[] _attractDemonMaterials;
    private Material[] _repelDemonMaterials;

    [Header("Repel Demon")]
    [SerializeField] private GameObject _repelDemonVisual;
    [SerializeField] private MeshRenderer _repelMeshRenderer;
    private Material[] _transparentMaterials;
    private Material[] _solidMaterials;

    [Space]
    
    private GameManager _gameManager;

    // delete if SetFloat() doesn't work
    private float pullOpacity;
    private float pushOpacity;
    
    private void Start()
    {
        // delete if SetFloat() doesn't work
        pullOpacity = 0.7f;
        pushOpacity = 1f;

        _gameManager = GameManager.Instance;;
        _attractDemonMaterials = _attractMeshRenderer.materials;
        _repelDemonMaterials = _repelMeshRenderer.materials;

        _transparentMaterials = new[] { new Material(_transparentTrailMaterial) };
        _solidMaterials = new[] { new Material(_solidTrailMaterial) };
    }

    private void Update()
    {
        if (_gameManager.pullingInDemons)
        {
            foreach (Material mat in _attractDemonMaterials)
            {
                // mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, 0f);
                if (mat.HasFloat("_Opacity Multiplier"))
                {
                    mat.SetFloat("_Opacity Multiplier", pullOpacity);
                }
            }
            // _attractDemonVisual.SetActive(true);
            // _repelDemonVisual.SetActive(false);

            _trailRenderer.materials = _transparentMaterials;
        }
        else
        {
            foreach (Material mat in _attractDemonMaterials)
            {
                // mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, 1f);
                if (mat.HasFloat("_Opacity Multiplier"))
                {
                    mat.SetFloat("_Opacity Multiplier", pushOpacity);
                }
            }
            // _attractDemonVisual.SetActive(false);
            // _repelDemonVisual.SetActive(true);

            _trailRenderer.materials = _solidMaterials;

        }
    }
}
