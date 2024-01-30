using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoadDimension : MonoBehaviour
{
    [SerializeField] private int _dimensionToLoadSceneIndex;

    [SerializeField] private intVariable _overworldDimensionSceneIndex;

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
                SceneManager.LoadScene(_dimensionToLoadSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(_overworldDimensionSceneIndex.Value);
            }
        }
    }
}
