using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingScreenFadeIn : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;

    private void Start()
    {
        _backgroundImage.color =
        new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 1f);
    }

    private void Update()
    {
        if (_backgroundImage.color.a <= 0.5f)
        {
            Debug.Log(_backgroundImage.color.a);
            _backgroundImage.color =
                new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, _backgroundImage.color.a - Time.deltaTime / 10f);
            Debug.Log("yuh");
        }
        else
        {
            _backgroundImage.color =
                new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, _backgroundImage.color.a - Time.deltaTime / 5f);
            Debug.Log("wahoo");

        }

        if (_backgroundImage.color.a >= 1f)
        {
            gameObject.SetActive(false);
        }
    }
}
