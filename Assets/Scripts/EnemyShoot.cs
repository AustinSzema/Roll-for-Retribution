using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Vector3Variable _playerPos;

    [SerializeField] private float _rotationSpeed = 100f;


    [SerializeField] private GameObject _bulletPrefab;
    
    private void Start()
    {
        StartCoroutine(Shoot(1f));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = _playerPos.Value - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }
    
    private IEnumerator coroutine;

    private IEnumerator Shoot(float waitTime)
    {

        GameObject bullet = Instantiate(_bulletPrefab, transform.position, quaternion.identity);

        bullet.GetComponent<Rigidbody>().linearVelocity = transform.forward * 100f;
        
        
        Debug.Log("bingus");
        
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Shoot(1f));
    }
    
    
}
