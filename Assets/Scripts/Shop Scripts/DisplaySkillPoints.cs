using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplaySkillPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillPointsText;
    private SkillPoints _skillPoints;

    private void Start()
    {
        _skillPoints = FindObjectOfType<SkillPoints>();
    }


    // Update is called once per frame
    void Update()
    {
        skillPointsText.text = "Soul Points: " + _skillPoints.skillPoints.ToString();
    }
}
