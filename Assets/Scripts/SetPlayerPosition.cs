using UnityEngine;

public class SetPlayerPosition : MonoBehaviour
{
    [SerializeField] private Vector3Variable playerPos; 
    
    void Update()
    {
        playerPos.Value = transform.position;
        
    }
}
