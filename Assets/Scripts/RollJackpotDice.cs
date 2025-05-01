using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollJackpotDice : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RolLDice(1f));
    }

    private IEnumerator RolLDice(float waitTime){
        yield return new WaitForSeconds(waitTime);
        
        rb.AddForce(Random.onUnitSphere * 100f);
    }
}
