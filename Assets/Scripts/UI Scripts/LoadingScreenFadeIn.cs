using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingScreenFadeIn : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;

    private void Start()
    {
        _backgroundImage.enabled = true;
        _backgroundImage.color =
        new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 1f);
    }

    private void Update()
    {
        if (_backgroundImage.color.a <= 0.5f)
        {
            _backgroundImage.color =
                new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, _backgroundImage.color.a - Time.deltaTime / 10f);
        }
        else
        {
            _backgroundImage.color =
                new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, _backgroundImage.color.a - Time.deltaTime / 5f);
        }

        if (_backgroundImage.color.a >= 1f)
        {
            _backgroundImage.enabled = false;
        }
    }
}
