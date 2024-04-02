using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPoints : MonoBehaviour
{

    [SerializeField] private intVariable killCount;

    public int skillPoints;

    [SerializeField]
    private int[] _thresholds =
    {
        125, 165, 245, 365, 525, 725, 965, 1245, 1565, 1925, 2325, 2765, 3245, 3765,
        4325, 4925, 5565, 6245, 6965, 7725
    };

    [SerializeField]
    private int[] SkillPointsToGainAtThreshold =
    {
        2, 3, 4, 5, 6, 7, 10, 13, 17, 21, 26, 31, 37, 44, 51, 58, 71, 80, 89, 98, 108
    };
    
    private Dictionary<int, int> _gainAtThreshold;

    private int _thresholdIdx;

    private void Start()
    {
        if (_thresholds.Length > SkillPointsToGainAtThreshold.Length)
        {
            throw new ArgumentException("Each threshold must have a corresponding number of skill points to gain");
        }
        _gainAtThreshold = new Dictionary<int, int>();
        for (int i = 0; i < _thresholds.Length; i++)
        {
            _gainAtThreshold.Add(_thresholds[i], SkillPointsToGainAtThreshold[i]);
        }
        _thresholdIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
       GiveSkillPoints(); 
    }

    void GiveSkillPoints()
    {
        if (killCount.Value > _thresholds[_thresholdIdx])
        {
            skillPoints += _gainAtThreshold[_thresholds[_thresholdIdx]];
            _thresholdIdx++;
        } 
    }

    public void SpendSkillPoints(int cost)
    {
        skillPoints -= cost;
    }

    
    
    
    
}
