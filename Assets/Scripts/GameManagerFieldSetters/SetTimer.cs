using System;
using System.Collections.Generic;

using TMPro;
using UnityEngine;

public class SetTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    

    [SerializeField] private RoundSpawnConfig _roundSpawnConfig;

    private float countdown = 1f;
    
    private void Start()
    {
        countdown = _roundSpawnConfig.Value[GameManager.Instance.currentRound] // round 1
            .Value[_roundSpawnConfig.Value[GameManager.Instance.currentRound].Value.Count-1].StartTime; // last element of round one

        _timerText.text = "" + countdown;
    }

    // Update is called once per frame
    void Update()
    {
        /*float timeInSeconds = Time.timeSinceLevelLoad;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        string timerString = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds);

        _timerText.text = timerString;*/


        _timerText.text = (countdown - Time.timeSinceLevelLoad).ToString();

    }
}