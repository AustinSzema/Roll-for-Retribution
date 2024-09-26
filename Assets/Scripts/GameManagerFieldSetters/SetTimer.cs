using System;
using TMPro;
using UnityEngine;

public class SetTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private RoundSpawnConfig _roundSpawnConfig;

    [SerializeField] private TextMeshProUGUI fiveSecondCountdown;
    
    private float countdown;

    private void Start()
    {
        if (GameManager.Instance.currentRound < _roundSpawnConfig.Value.Count)
        {
            if (_roundSpawnConfig.Value[GameManager.Instance.currentRound].Value.Count < 2)
            {
                throw new Exception("Spawn info variable must have at least 2 elements for the timer to work");
            }
        } 
        
        // Assuming StartTime is the time in seconds for the current round
        countdown = _roundSpawnConfig.Value[GameManager.Instance.currentRound]
            .Value[_roundSpawnConfig.Value[GameManager.Instance.currentRound].Value.Count - 1].StartTime;

        UpdateTimerText(countdown);
    }

    private void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            UpdateTimerText(countdown);
        }
        else
        {
            countdown = 0;
            UpdateTimerText(countdown); // Final update to ensure timer shows 0 when complete
        }
    }

    private void UpdateTimerText(float timeInSeconds)
    {
        // Convert to TimeSpan to easily format minutes and seconds
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        // Display minutes and seconds (MM:SS format)
        string timerString = string.Format("{0:D2}:{1:D2}",
            timeSpan.Minutes,
            timeSpan.Seconds);

        if (timeSpan.Minutes < 1 && timeSpan.Seconds <= 5)
        {
            fiveSecondCountdown.gameObject.SetActive(true);
            _timerText.enabled = false;
            fiveSecondCountdown.text = timeSpan.Seconds.ToString();
            if (timeSpan.Seconds < 1)
            {
                //TODO: fix this hard coded garbage
                fiveSecondCountdown.rectTransform.sizeDelta = new Vector2(1600f,fiveSecondCountdown.rectTransform.sizeDelta.y);
                fiveSecondCountdown.fontSize = 300;
                fiveSecondCountdown.text = "Round " + (GameManager.Instance.currentRound + 1) + " Complete";
            }
        }
        
        _timerText.text = timerString;
    }
    
    
}