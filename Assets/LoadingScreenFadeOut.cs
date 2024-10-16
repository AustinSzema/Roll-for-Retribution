using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenFadeOut : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;

    private void Start()
    {
        _backgroundImage.enabled = true;
        _backgroundImage.color =
            new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 0f);
    }

    private void Update()
    {
        _backgroundImage.color =
            new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, _backgroundImage.color.a + Time.deltaTime * 5f);

        if (_backgroundImage.color.a >= 1f)
        {
            GameManager.Instance.currentRound++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

