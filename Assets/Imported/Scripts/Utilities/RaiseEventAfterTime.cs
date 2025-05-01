using System.Threading.Tasks;
using UnityEngine;

public class RaiseEventAfterTime : MonoBehaviour
{
    [SerializeField] private GameEvent _eventToRaise;

    public async void WaitAndRaise(float timeToWaitInSeconds)
    {
        await Task.Delay(Mathf.RoundToInt(timeToWaitInSeconds * 1000));
        _eventToRaise.Raise();
    }
}
