using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerLevelState _playerLevelState;
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private GameEvent _onDeathEvent;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _playerLevelState.IsBehindCage = false;
        _playerLevelState.IsInCage = false;
    }

    public void TakeLightningDamage(int damageTaken)
    {
        if (_playerLevelState.IsInCage)
        {
            return;
        }
        TakeDamage(damageTaken);
    }

    public void TakeFireDamage(int damageTaken)
    {
        if (_playerLevelState.IsBehindCage)
        {
            return;
        }
        TakeDamage(damageTaken);
    }

    public void TakeDamage(int damageTaken)
    {
        if (_currentHealth - damageTaken > 0)
        {
            _currentHealth -= damageTaken;
            Debug.Log(string.Format("Damage Taken! You now have {0} health.", _currentHealth));
        }
        else
        {
            _currentHealth = 0;
            Debug.Log("You died, friend!");
            _onDeathEvent.Raise();
        }
    }
}
