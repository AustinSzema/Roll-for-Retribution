using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetKillCount : MonoBehaviour
{
    [SerializeField] private intVariable _killCount;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetKilLCount();
    }

    public void ResetKilLCount()
    {
        _killCount.Value = 0;
    }
}
