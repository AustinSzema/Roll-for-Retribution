using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerLevelState : ScriptableObject
{
    [SerializeField]
    private bool _isInCage = false;
    [SerializeField]
    private bool _isBehindCage = false;
    public bool IsInCage
    {
        get
        {
            return _isInCage;
        }
        set
        {
            _isInCage = value;
        }
    }
    public bool IsBehindCage
    {
        get
        {
            return _isBehindCage;
        }
        set
        {
            _isBehindCage = value;
        }
    }
}
