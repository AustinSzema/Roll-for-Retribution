using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackParent : MonoBehaviour
{

    [SerializeField] private GameObject healthPackObject;
    public void CollectPack()
    {
        StartCoroutine(ResetHealthPack());
    }
    
    private IEnumerator ResetHealthPack()
    {
        yield return new WaitForSeconds(20f);
        healthPackObject.SetActive(true);
    }
}
