using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateBehavior : MonoBehaviour
{
    [SerializeField] private float aggroDistance;
    [SerializeField] private int shootForce;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int shootCooldown;
    private float _distToPlayer;
    private Transform _playerTransform;
    private bool _shooting;
    void Start()
    {
      _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
      _shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
      _distToPlayer = Vector3.Distance(_playerTransform.position, transform.position);
      if (_distToPlayer < aggroDistance && !_shooting)
      {
        StartCoroutine(Shoot());
      }
      else
      {
        Vector3.MoveTowards(transform.position, _playerTransform.position, 2f);
      }
    }

    IEnumerator Shoot()
    {
      _shooting = true;
      GameObject bullet = Instantiate(projectile);
      bullet.GetComponent<Rigidbody>().AddForce(shootForce * (_playerTransform.position - transform.position).normalized);
      yield return new WaitForSeconds(shootCooldown);
      _shooting = false;
    }
}
