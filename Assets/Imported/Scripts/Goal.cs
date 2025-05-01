using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [HideInInspector] public bool reachedGoal = false;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI scoreText;

    [HideInInspector] public static Goal Instance;

    private void Awake()
    {
        Instance = this;
    }

    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            score++;
            if (other.gameObject.GetComponent<CarController>())
            {
                reachedGoal = true;
                scoreText.text = "You came in " + score + "st place";
                winScreen.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
        }
    }
}
