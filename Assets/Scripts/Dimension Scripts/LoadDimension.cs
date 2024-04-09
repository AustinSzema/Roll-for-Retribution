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

    [Header("Loading Screen")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Slider _loadingSlider;
    private void Start()
    {
        _overworldDimensionSceneIndex.Value = SceneManager.GetActiveScene().buildIndex;
        
        _backgroundImage.color =
            new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 0f);
        _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _loadingSlider.gameObject.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex != _dimensionToLoadSceneIndex)
            {
                FadeIn();
                StartCoroutine(LoadSceneAsync(_dimensionToLoadSceneIndex));   
            }
            else
            {
                FadeOut();
                StartCoroutine(LoadSceneAsync(_overworldDimensionSceneIndex.Value));   

            }
        }
    }
    

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 100f); // 0.9f is the maximum progress value
            _loadingSlider.value = progress;
            _loadingText.text = "Loading " + (progress * 100f).ToString("0") + "%";

            yield return null;
        }
    }

    private float _loadingStartTime = 0f;
    private float _fadeRate = 0.01f;

    private void FadeIn()
    {
        _backgroundImage.color =
            new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 0f);
        _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, 0f);
        InvokeRepeating(nameof(AddAlpha), _loadingStartTime, _fadeRate);
    }

    private void AddAlpha()
    {
        _backgroundImage.color =
            new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, _backgroundImage.color.a + 1);
        _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, _loadingText.color.a + 1);

        Debug.Log("Fading In");

        if (_backgroundImage.color.a >= 255 && _loadingText.color.a >= 255)
        {
            CancelInvoke(nameof(FadeIn));
        }

    }
    
    private void FadeOut()
    {
        _backgroundImage.color =
            new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 255f);
        _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, 255f);
        InvokeRepeating(nameof(SubtractAlpha), _loadingStartTime, _fadeRate);
    }

    private void SubtractAlpha()
    {
        _backgroundImage.color =
            new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, _backgroundImage.color.a - 1);
        _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, _loadingText.color.a - 1);
        
        Debug.Log("Fading Out");
        
        if (_backgroundImage.color.a <= 0 && _loadingText.color.a <= 0)
        {
            CancelInvoke(nameof(FadeOut));
        }
    }
}
