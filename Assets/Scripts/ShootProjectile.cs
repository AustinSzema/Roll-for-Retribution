using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private float shootInterval = 1f;
    [SerializeField] private float shootSpeed = 100f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }


    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootInterval);
        GameObject proj = Instantiate(projectile, transform.position + new Vector3(0f, 10f, 0f), Quaternion.identity);
        Rigidbody rb = proj.GetComponent<Rigidbody>(); 
        if (rb != null)
        {
            Vector3 playerDirection = GameManager.Instance.playerPosition - transform.position;

            rb.AddForce(playerDirection * shootSpeed);
        }

        StartCoroutine(Shoot());
    }
}
