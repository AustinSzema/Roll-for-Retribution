using UnityEngine;

public class PlayerOpenDoor : MonoBehaviour
{
    public Camera playerCamera;
    private float rayDistance = Mathf.Infinity;
    public LayerMask interactableLayer;

    private void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, interactableLayer))
        {

            Door door = hit.collider.gameObject.GetComponent<Door>();

            if (door)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    door.TriggerDoor();
                    
                }
                
            }
        }
        



    }
    
}

