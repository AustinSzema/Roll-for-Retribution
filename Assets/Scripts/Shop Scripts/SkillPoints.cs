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
        _gainAtThreshold = new Dictionary<int, int>();
        _gainAtThreshold.Add(_thresholds[0], SkillPointsToGainAtThreshold[0]); // 1
        _gainAtThreshold.Add(_thresholds[1], SkillPointsToGainAtThreshold[1]); // 2
        _gainAtThreshold.Add(_thresholds[2], SkillPointsToGainAtThreshold[2]); // 3
        _gainAtThreshold.Add(_thresholds[3], SkillPointsToGainAtThreshold[3]); // 4
        _gainAtThreshold.Add(_thresholds[4], SkillPointsToGainAtThreshold[4]); // 5
        _gainAtThreshold.Add(_thresholds[5], SkillPointsToGainAtThreshold[5]); // 6
        _gainAtThreshold.Add(_thresholds[6], SkillPointsToGainAtThreshold[6]); // 7
        _gainAtThreshold.Add(_thresholds[7], SkillPointsToGainAtThreshold[7]); // 8
        _gainAtThreshold.Add(_thresholds[8], SkillPointsToGainAtThreshold[8]); // 9
        _gainAtThreshold.Add(_thresholds[9], SkillPointsToGainAtThreshold[9]); // 10
        _gainAtThreshold.Add(_thresholds[10], SkillPointsToGainAtThreshold[10]); // 11
        _gainAtThreshold.Add(_thresholds[11], SkillPointsToGainAtThreshold[11]); // 12
        _gainAtThreshold.Add(_thresholds[12], SkillPointsToGainAtThreshold[12]); // 13
        _gainAtThreshold.Add(_thresholds[13], SkillPointsToGainAtThreshold[13]); // 14
        _gainAtThreshold.Add(_thresholds[14], SkillPointsToGainAtThreshold[14]); // 15
        _gainAtThreshold.Add(_thresholds[15], SkillPointsToGainAtThreshold[15]); // 16
        _gainAtThreshold.Add(_thresholds[16], SkillPointsToGainAtThreshold[16]); // 17
        _gainAtThreshold.Add(_thresholds[17], SkillPointsToGainAtThreshold[17]); // 18
        _gainAtThreshold.Add(_thresholds[18], SkillPointsToGainAtThreshold[18]); // 19
        _gainAtThreshold.Add(_thresholds[19], SkillPointsToGainAtThreshold[19]); // 20

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
