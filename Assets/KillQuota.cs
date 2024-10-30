using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillQuota : MonoBehaviour
{
    [SerializeField] private Slider killQuotaSlider;

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
}
