using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillQuota : MonoBehaviour
{
    public Slider killQuotaSlider;
    public Color filledSliderColor;
    public Image targetImage;

    [SerializeField] private GameObject portal;
    
    public static KillQuota Instance;
    
    private int killCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddKills(int killAmount)
    {
        killCount += killAmount;
    }

    // Update is called once per frame
    void Update()
    {
        killQuotaSlider.value = killCount;
    }

    public void EnablePortal()
    {
        if (killQuotaSlider.value >= killQuotaSlider.maxValue)
        {
            portal.SetActive(true);
            targetImage.color = filledSliderColor;
        }
    }
}
