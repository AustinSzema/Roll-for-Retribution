using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCaster : MonoBehaviour
{
    [SerializeField] private GameObject _shieldPrefab;

    [SerializeField] private Vector3Variable _playerPos;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CastShield();
        }
        
    }

    private void CastShield()
    {
        GameObject shield = Instantiate(_shieldPrefab, _playerPos.Value + Camera.main.transform.forward * 10f,  Camera.main.transform.rotation);
     }
}
