using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public enum SkillsToLevel
    {
        PullForce = 0,
        DiceQuantity = 1,
        DiceWeight = 2
    }

    [SerializeField]
    private SkillPoints _skillPoints;

    // this is the cost to upgrade a a skill when we are at a given level
    // e.g when we are at level 0, it costs 5 to go to level 1
    // e.g when we are at level 1, it costs 5 to go to level 2
    // e.g when we are at level 2, it costs 10 to go to level 3
    [SerializeField]
    private int[] _skillCosts = { 5, 5, 10, 10, 15, 15, 20, 20, 25, 25 };

    // These are the current levels of our skills
    // the idx corresponds to the number ascribed in SkilsToLevel
    // _skillLevels[0] is the PullForce level since in the enum
    // SkillsToLevel, pull force is assigned to zero
    private int[] _skillLevels = { 0, 0, 0 };

    [SerializeField]
    private int[] _cubesToAddAtLevel = {1, 2, 3, 4, 5, 5, 6, 6, 8, 10 };

    [SerializeField]
    private int[] _pullForceAtLevel = { 61, 62, 64, 66, 69, 72, 76, 80, 85, 90 };

    [SerializeField]
    private float[] _percentDecreaseAtLevel = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, };

    private Magnet _playerMagnet;
    

    // Start is called before the first frame update
    void Start()
    {
        _playerMagnet = GameObject.FindObjectOfType<Magnet>();
    }

    int CurrentLevel(SkillsToLevel skill)
    {
        return _skillLevels[(int) skill];
    }

    public int CostToLevel(SkillsToLevel skill)
    {
        int currentLevel = CurrentLevel(skill);

        // if we have reached the maximum level for this skill
        // return maxval for int
        if (currentLevel > _skillCosts.Length)
        {
            return Int32.MaxValue;
        }

        return _skillCosts[currentLevel];
    }

    // Returns true if we have enough skill points to level up
    // the given skill, and we arent at max level, otherwise false
    bool CanLevelUp(SkillsToLevel skill)
    {
        return _skillPoints.skillPoints >= CostToLevel(skill) && CurrentLevel(skill) < _skillCosts.Length;
    }

    public void LevelUp(SkillsToLevel skill)
    {
        // if we cannot afford to level up the skill, we dont
        if (!CanLevelUp(skill))
        {
            return;
        }

        switch (skill)
        {
           case SkillsToLevel.DiceQuantity:
               for (int i = 0; i < _cubesToAddAtLevel[CurrentLevel(skill)]; i++)
               {
                   _playerMagnet.AddMagneticCube();
               }
               break;
           case SkillsToLevel.DiceWeight:
               _playerMagnet.DecreaseDiceWeight(_percentDecreaseAtLevel[CurrentLevel(skill)]);
               break;
           case SkillsToLevel.PullForce:
               _playerMagnet.PullSpeed = _pullForceAtLevel[CurrentLevel(skill)];
               break;
           default:
               throw new NotImplementedException();
        }

        _skillPoints.SpendSkillPoints(CostToLevel(skill));
        _skillLevels[(int)skill]++;
    }
    
    public void LevelQuantity() { LevelUp((SkillsToLevel.DiceQuantity));}
    public void LevelWeight() { LevelUp((SkillsToLevel.DiceWeight));}
    public void LevelPullForce() { LevelUp((SkillsToLevel.PullForce));}

    public int CurrentSkillPoints()
    {
        return _skillPoints.skillPoints;
    }
    
}
