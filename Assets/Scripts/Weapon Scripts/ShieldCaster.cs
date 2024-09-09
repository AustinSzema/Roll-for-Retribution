using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCaster : MonoBehaviour
{
    [SerializeField] private GameObject _shieldPrefab;

    private GameManager _gameManager;

    private bool canShoot = true;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && canShoot)
        {
            CastShield();
        }
        
    }

    private void CastShield()
    {
        StartCoroutine(CastMultipleShields(3));
    }

    private IEnumerator CastMultipleShields(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject shield = Instantiate(_shieldPrefab, _gameManager.playerPosition + Camera.main.transform.forward * 10f,  Camera.main.transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }
        canShoot = false;
        StartCoroutine(ShieldCooldown(0.5f));
    }

    private IEnumerator ShieldCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
