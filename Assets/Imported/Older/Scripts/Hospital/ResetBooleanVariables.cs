using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBooleanVariables : MonoBehaviour
{
    [SerializeField] private BoolVariable[] _variablesToReset;
    // Start is called before the first frame update
    void Start()
    {
        foreach (BoolVariable variable in _variablesToReset)
        {
            variable.Value = false;
        }
    }


}
