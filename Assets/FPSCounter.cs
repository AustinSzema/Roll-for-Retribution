using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsCounterText;

    private float deltaTime = 0.0f;
    
    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime * 0.1f);
        int fps = Mathf.RoundToInt(1.0f / deltaTime);

        fpsCounterText.text = fps + " FPSe";
    }

}
