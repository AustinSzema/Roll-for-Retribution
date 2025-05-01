using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShadow : MonoBehaviour
{

    [SerializeField] private GameObject shadow;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            shadow.SetActive(true);
            shadow.transform.position = new Vector3(player.transform.position.x, hit.transform.position.y, player.transform.position.z);
        }
        else
        {
            shadow.SetActive(false);
        }
    }


}
