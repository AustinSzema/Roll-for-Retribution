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
        // Check if currentRound exists in the _roundSpawnConfig
        if (GameManager.Instance.currentRound < _roundSpawnConfig.Value.Count)
        {
            var spawnInfoVariable = _roundSpawnConfig.Value[GameManager.Instance.currentRound];
        
            // Check if the Value (List<SpawnInfo>) in the spawnInfoVariable is not null or empty
            if (spawnInfoVariable.Value != null && spawnInfoVariable.Value.Count > 0)
            {
                // Now safely access the last element's StartTime
                countdown = spawnInfoVariable.Value[spawnInfoVariable.Value.Count - 1].StartTime;
            }
            else
            {
                // Handle the case where there's no spawn info for this round
                Debug.LogError("No spawn info available for the current round.");
                countdown = 0; // Set a default value or handle accordingly
            }
        }
        else
        {
            var spawnInfoVariable = _roundSpawnConfig.Value[GameManager.Instance.currentRound-1];
        
            // Check if the Value (List<SpawnInfo>) in the spawnInfoVariable is not null or empty
            if (spawnInfoVariable.Value != null && spawnInfoVariable.Value.Count > 0)
            {
                // Now safely access the last element's StartTime
                countdown = spawnInfoVariable.Value[spawnInfoVariable.Value.Count - 1].StartTime;
            }

            
            // Handle the case where the currentRound is out of bounds
            //Debug.LogError("Current round exceeds available round configurations.");
            //countdown = 0; // Set a default value or handle accordingly
        }

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