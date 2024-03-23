using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPoints : MonoBehaviour
{

    [SerializeField] private intVariable killCount;

    public int skillPoints;

    private readonly int[] _thresholds =
    {
        125, 165, 245, 365, 525, 725, 965, 1245, 1565, 1925, 2325, 2765, 3245, 3765,
        4325, 4925, 5565, 6245, 6965, 7725
    };

    private Dictionary<int, int> _gainAtThreshold;

    private int _thresholdIdx;

    private void Start()
    {
        _gainAtThreshold = new Dictionary<int, int>();
        _gainAtThreshold.Add(_thresholds[0], 2); // 1
        _gainAtThreshold.Add(_thresholds[1], 3); // 2
        _gainAtThreshold.Add(_thresholds[2], 4); // 3
        _gainAtThreshold.Add(_thresholds[3], 5); // 4
        _gainAtThreshold.Add(_thresholds[4], 7); // 5
        _gainAtThreshold.Add(_thresholds[5], 10); // 6
        _gainAtThreshold.Add(_thresholds[6], 13); // 7
        _gainAtThreshold.Add(_thresholds[7], 17); // 8
        _gainAtThreshold.Add(_thresholds[8], 21); // 9
        _gainAtThreshold.Add(_thresholds[9], 26); // 10
        _gainAtThreshold.Add(_thresholds[10], 31); // 11
        _gainAtThreshold.Add(_thresholds[11], 37); // 12
        _gainAtThreshold.Add(_thresholds[12], 44); // 13
        _gainAtThreshold.Add(_thresholds[13], 51); // 14
        _gainAtThreshold.Add(_thresholds[14], 58); // 15
        _gainAtThreshold.Add(_thresholds[15], 71); // 16
        _gainAtThreshold.Add(_thresholds[16], 80); // 17
        _gainAtThreshold.Add(_thresholds[17], 89); // 18
        _gainAtThreshold.Add(_thresholds[18], 98); // 19
        _gainAtThreshold.Add(_thresholds[19], 108); // 20

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
