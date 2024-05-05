using System;
using TMPro;
using UnityEngine;

public class SetTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    // Update is called once per frame
    void Update()
    {
        float timeInSeconds = Time.timeSinceLevelLoad;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        string timerString = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds);

        _timerText.text = timerString;
    }
}