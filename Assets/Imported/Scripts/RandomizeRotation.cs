using UnityEngine;

public class RandomizeRotation : MonoBehaviour
{
    void Start()
    {
        transform.rotation = Random.rotation;
    }

}
