using UnityEngine;

public class OffsetChildPositions : MonoBehaviour
{
    [SerializeField] private Transform[] children;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in children)
        {
            child.position += Random.onUnitSphere * 20;
        }
    }
}
