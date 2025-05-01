using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCrate : MonoBehaviour
{

    private Vector3 originalPos;

    [SerializeField] private Collider collider;
    [SerializeField] private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -25f)
        {
            transform.position = originalPos;
            transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            collider.enabled = true;
            meshRenderer.enabled = true;
        }
    }
}
