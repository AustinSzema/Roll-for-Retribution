using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetKillCountOnStart : MonoBehaviour
{
    [SerializeField] private intVariable _killCount;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetKillCount();
    }

    public void ResetKillCount()
    {
        _killCount.Value = 0;
    }
}
