using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBlackBoxDimension : MonoBehaviour
{
    [SerializeField] private int _blackBoxDimensionSceneIndex;

    [SerializeField] private intVariable _previousDimensionSceneIndex;

    private void Start()
    {
        _previousDimensionSceneIndex.Value = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex != _blackBoxDimensionSceneIndex)
            {
                SceneManager.LoadScene(_blackBoxDimensionSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(_previousDimensionSceneIndex.Value);
                //SceneManager.LoadScene(0);
            }
        }
    }
}
