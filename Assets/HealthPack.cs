using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{


    [SerializeField] private HealthPackParent healthPackParent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.playerTag))
        {
            Heal();
            healthPackParent.CollectPack();
            gameObject.SetActive(false);
        }
    }

    public void Heal()
    {
        GameManager.Instance.HealPlayer();
    }

}
