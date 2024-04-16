using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadDimension : MonoBehaviour
{
    [SerializeField] private int _dimensionToLoadSceneIndex;

    [SerializeField] private intVariable _overworldDimensionSceneIndex;

    [SerializeField] private LoadingScreen _loadingScreen;
    
    private void Start()
    {
        _overworldDimensionSceneIndex.Value = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _loadingScreen._loadingSlider.gameObject.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex != _dimensionToLoadSceneIndex)
            {
                _loadingScreen.FadeIn();
                StartCoroutine(_loadingScreen.LoadSceneAsync(_dimensionToLoadSceneIndex));   
            }
            else
            {
                _loadingScreen.FadeOut();
                StartCoroutine(_loadingScreen.LoadSceneAsync(_overworldDimensionSceneIndex.Value));   
            }
        }
    }
}
