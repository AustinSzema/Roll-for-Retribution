using UnityEngine;

public class SetGameIsPausedOnStart : MonoBehaviour
{
    [SerializeField] private boolVariable _gameIsPaused;
    private void Start() 
    {
        _gameIsPaused.Value = false;
    }

}
