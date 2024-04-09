using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoadDimension : MonoBehaviour
{
    [SerializeField] private int _dimensionToLoadSceneIndex;

    [SerializeField] private intVariable _overworldDimensionSceneIndex;

    [SerializeField] private GameObject _loadingScreen;
    
    private void Start()
    {
        _overworldDimensionSceneIndex.Value = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex != _dimensionToLoadSceneIndex)
            {
                _loadingScreen.SetActive(true);
                SceneManager.LoadScene(_dimensionToLoadSceneIndex);
            }
            else
            {
                _loadingScreen.SetActive(true);
                SceneManager.LoadScene(_overworldDimensionSceneIndex.Value);
            }
        }
    }
}
