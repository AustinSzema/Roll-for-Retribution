using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetKillCountText : MonoBehaviour
{

    [SerializeField] private intVariable _killCount;
    
    [SerializeField] private TextMeshProUGUI _killCountText;

    // Update is called once per frame
    void Update()
    {
        _killCountText.text = "Kill Count: " + _killCount.Value;

    }
}
