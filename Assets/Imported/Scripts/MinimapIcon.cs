using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    private Transform followTarget;
    // Start is called before the first frame update
    void Start()
    {
        followTarget = transform.parent;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(followTarget.position.x, transform.position.y, followTarget.position.z);
    }
}
