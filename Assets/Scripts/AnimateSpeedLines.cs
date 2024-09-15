using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSpeedLines : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private void Start()
    {
        StartCoroutine(AnimateLines());
    }
    private IEnumerator AnimateLines()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y * -1,
                rectTransform.localScale.z);   
        }
    }
}
 