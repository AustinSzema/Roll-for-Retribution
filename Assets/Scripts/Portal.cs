using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreenFadeOut;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.playerTag))
        {
            GameManager.Instance.gameIsPaused = true;
            loadingScreenFadeOut.SetActive(true);
        }
    }
}

