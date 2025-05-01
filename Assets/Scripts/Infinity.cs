using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;

public class Infinity : MonoBehaviour
{

    [SerializeField] private Vector3Variable playerPos;
    // Update is called once per frame
    void Update()
    {

        //float distance = Mathf.Abs(Mathf.Log(Vector3.Distance(transform.position, playerPos.Value) / 2f));
        float distance = Vector3.Distance(transform.position, playerPos.Value) - 1f;
        if(distance < Mathf.Epsilon)
        {
            distance = Mathf.Epsilon;
        }
        Debug.Log(distance);
        transform.localScale = new Vector3 (distance, distance, distance);

        //transform.localScale = new Vector3(distance - transform.localScale.x, distance - transform.localScale.y, distance - transform.localScale.z);



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("You Have Reached Infinity");
        }
    }
}



/*
 * 
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;

public class Infinity : MonoBehaviour
{

    [SerializeField] private Vector3Variable playerPos;
    // Update is called once per frame

    private float _xStartScale;
    private float _yStartScale;
    private float _zStartScale;

    private void Start()
    {
        _xStartScale = transform.localScale.x;
        _yStartScale = transform.localScale.y;
        _zStartScale = transform.localScale.z;
    }
    void Update()
    {

        //float distance = Mathf.Abs(Mathf.Log(Vector3.Distance(transform.position, playerPos.Value) / 2f));
        float distanceX = Vector3.Distance(transform.position, playerPos.Value) - _xStartScale;
        float distanceY = Vector3.Distance(transform.position, playerPos.Value) - _yStartScale;
        float distanceZ = Vector3.Distance(transform.position, playerPos.Value) - _zStartScale;

        if (distanceX < Mathf.Epsilon)
        {
            distanceX = Mathf.Epsilon;
        }
        if (distanceY < Mathf.Epsilon)
        {
            distanceY = Mathf.Epsilon;
        }
        if (distanceZ < Mathf.Epsilon)
        {
            distanceZ = Mathf.Epsilon;
        }

        transform.localScale = new Vector3 (distanceX, distanceX, distanceZ);

        //transform.localScale = new Vector3(distance - transform.localScale.x, distance - transform.localScale.y, distance - transform.localScale.z);



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("You Have Reached Infinity");
        }
    }
}
*/
