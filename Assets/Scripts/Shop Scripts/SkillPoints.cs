using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPoints : MonoBehaviour
{

    [SerializeField] private AudioClip _progressionClip1;
    [SerializeField] private AudioClip _progressionClip2;
    [SerializeField] private AudioClip _progressionClip3;
    [SerializeField] private AudioClip _progressionClip4;
    [SerializeField] private AudioClip _progressionClip5;


    private GameManager _gameManager;
    
    private static AudioManager _audioManager;

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
        _gameManager = GameManager.Instance;
        _audioManager = FindObjectOfType<AudioManager>();
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
        if (_gameManager.killCount > _thresholds[_thresholdIdx])
        {
            if(_thresholdIdx == 0) {
                _audioManager.PlayInvariableSFX(_progressionClip1);
            } else if (_thresholdIdx == 1) {
                _audioManager.PlayInvariableSFX(_progressionClip2);
            } else if (_thresholdIdx == 2) {
                _audioManager.PlayInvariableSFX(_progressionClip3);
            } else if (_thresholdIdx == 3) {
                _audioManager.PlayInvariableSFX(_progressionClip4);
            } else {
                _audioManager.PlayInvariableSFX(_progressionClip5);
            }

            skillPoints += _gainAtThreshold[_thresholds[_thresholdIdx]];
            _thresholdIdx++;
        } 
    }

    public void SpendSkillPoints(int cost)
    {
        skillPoints -= cost;
    }

    
    
    
    
}
