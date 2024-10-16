using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SetTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private RoundSpawnConfig _roundSpawnConfig;
    [SerializeField] private TextMeshProUGUI fiveSecondCountdown;
    [SerializeField] private GameObject loadingScreenFadeOut;
    
    private float countdown;

    private bool complete = false;

    private void Start()
    {
        // Check if currentRound exists in the _roundSpawnConfig
        if (GameManager.Instance.currentRound < _roundSpawnConfig.Value.Count)
        {
            var spawnInfoVariable = _roundSpawnConfig.Value[GameManager.Instance.currentRound];
        
            // Check if the Value (List<SpawnInfo>) in the spawnInfoVariable is not null or empty
            if (spawnInfoVariable.Value != null && spawnInfoVariable.Value.Count > 0)
            {
                countdown = spawnInfoVariable.Value[spawnInfoVariable.Value.Count - 1].StartTime;
            }
            else
            {
                Debug.LogError("No spawn info available for the current round.");
                countdown = 0;
            }
        }
        else
        {
            var spawnInfoVariable = _roundSpawnConfig.Value[GameManager.Instance.currentRound - 1];
        
            if (spawnInfoVariable.Value != null && spawnInfoVariable.Value.Count > 0)
            {
                countdown = spawnInfoVariable.Value[spawnInfoVariable.Value.Count - 1].StartTime;
            }
        }

        UpdateTimerText(countdown);

        // Start the countdown coroutine
        StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            UpdateTimerText(countdown);
            yield return null; // Wait for the next frame
        }

        // Ensure timer shows 0 when complete
        countdown = 0;
        UpdateTimerText(countdown);
    }

    private void UpdateTimerText(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        string timerString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        if (timeSpan.Minutes < 1 && timeSpan.Seconds <= 5)
        {
            fiveSecondCountdown.gameObject.SetActive(true);
            _timerText.enabled = false;
            fiveSecondCountdown.text = timeSpan.Seconds.ToString();
            if (timeSpan.Seconds <= 0)
            {
                // Update visuals when timer hits zero
                fiveSecondCountdown.rectTransform.sizeDelta = new Vector2(1600f, fiveSecondCountdown.rectTransform.sizeDelta.y);
                fiveSecondCountdown.fontSize = 300;
                fiveSecondCountdown.text = "Round " + (GameManager.Instance.currentRound + 1) + " Complete";
                GameManager.Instance.gameIsPaused = true;
                loadingScreenFadeOut.SetActive(true);
                enabled = false;
            }
        }
        
        _timerText.text = timerString;
    }
}
