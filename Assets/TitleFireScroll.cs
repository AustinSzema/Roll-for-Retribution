using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFireScroll : MonoBehaviour
{


    private float scrollSpeed = 500f;

    [SerializeField] private RectTransform rectTransform;

    private float startWidth = 1920;
    
    // Update is called once per frame
    void Update()
    {
        if (rectTransform.sizeDelta.x < Int32.MaxValue)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + Time.deltaTime * scrollSpeed, rectTransform.sizeDelta.y);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(startWidth, rectTransform.sizeDelta.y);
        }
    }
}
