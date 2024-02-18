using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    
    [SerializeField] private boolVariable _gameIsPaused;
    
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 0.25f);
    }
    

    private void SpawnEnemy()
    {

        if (!_gameIsPaused.Value)
        {
            GameObject enemy = ObjectPool.instance.GetPooledObject();
            if (enemy != null)
            {
                float maxDistanceFromPlayer = 25f;
                float minDistanceFromPlayer = 30f;

                float xOffset = Random.Range(maxDistanceFromPlayer, minDistanceFromPlayer) * Mathf.Sign(Random.Range(-1f, 1f));
                float zOffset = Random.Range(maxDistanceFromPlayer, minDistanceFromPlayer) * Mathf.Sign(Random.Range(-1f, 1f));

//                Debug.Log("Cow X Offset" + xOffset + "          Cow Z Offset" + zOffset);
                
                GameObject cow = enemy.GetComponentInChildren<Enemy>(true).gameObject;
                
                cow.transform.position = _playerTransform.position + new Vector3(_playerTransform.position.x + xOffset, 20f,_playerTransform.position.z + zOffset);
            
                
                
                cow.gameObject.SetActive(true);
            }
            
        }
        
    }
}