using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position += Vector3.up * 100f;
        //offset = transform.position - player.position;
        //transform.parent = null;

    }

    /*// Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset + Vector3.up * 100f;
    }*/
}
