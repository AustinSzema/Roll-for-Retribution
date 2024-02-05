using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTimer : MonoBehaviour
{

    
    [SerializeField] private TextMeshProUGUI _timerText;

    // Update is called once per frame
    void Update()
    {
      _timerText.text = Time.time.ToString("F2");
    }
}
