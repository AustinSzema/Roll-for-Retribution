using TMPro;
using UnityEngine;

public class OKComputer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    // Update is called once per frame
    void Update()
    {
        int daysLeft = DayManagerSingleton.Instance.lastDay - DayManagerSingleton.Instance.day + 1;
        string s = daysLeft != 1 ? "s" : "";
        dayText.text = daysLeft + " day" + s + " remaining until software update is complete";
    }
}
