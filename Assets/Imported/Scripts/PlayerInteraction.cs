using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float rayDistance = 3f;
    public LayerMask interactableLayer;
    public Transform handTransform;
    public Material highlightMaterial;

    private GameObject currentTarget;
    private Material originalMaterial;
    private Renderer targetRenderer;
    private GrabDetection currentGrabDetection;

    private bool objectIsGrabbed = false;


    private GrabDetection currentHover;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentGrabDetection)
            {
                currentTarget = null;
                currentGrabDetection.OnRelease();
                objectIsGrabbed = false;
                
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (!objectIsGrabbed && Physics.Raycast(ray, out RaycastHit hit, rayDistance, interactableLayer))
            {
                GameObject hitObj = hit.collider.transform.root.gameObject;
                GrabDetection grabDetection = hitObj.GetComponent<GrabDetection>();

                
                currentGrabDetection = grabDetection;
                if (grabDetection != null)
                {
                    currentTarget = hit.collider.gameObject.transform.parent.gameObject;
                    grabDetection.OnGrab();
                    hitObj.transform.parent = handTransform;
                    hitObj.transform.localPosition = Vector3.zero;
                    objectIsGrabbed = true;
                }
            }
        }
        
        
        
        Ray hoverRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (!objectIsGrabbed && Physics.Raycast(hoverRay, out RaycastHit hoverHit, rayDistance, interactableLayer))
        {
            GameObject hitObj = hoverHit.collider.transform.root.gameObject;
            GrabDetection grabDetection = hitObj.GetComponent<GrabDetection>();

            

            if (grabDetection != null)
            {
                currentHover = grabDetection;
                grabDetection.OnHoverEnter();
            }
            else
            {
                if (currentHover)
                {
                    currentHover.OnHoverExit();
                    currentHover = null;
                }
            }
        
        }


    }


    private void GrabObject(GameObject obj)
    {
        objectIsGrabbed = true;
        if (currentGrabDetection != null)
        {
            currentGrabDetection.OnGrab();
            
            // Set parent to handTransform and reset local position and rotation
            obj.transform.SetParent(handTransform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            // Disable physics (isKinematic) and remove gravity while object is held
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }

    private void ReleaseObject()
    {
        objectIsGrabbed = false;
        if (currentGrabDetection != null)
        {
            currentGrabDetection.OnRelease();

            // Unparent from hand and restore to original parent
            currentTarget.transform.SetParent(null);

            // Re-enable physics and gravity
            Rigidbody rb = currentTarget.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            // Optionally, reset object position and rotation when released
            // currentTarget.transform.position = new Vector3(...); // Set position if needed

            // Clear references
            currentGrabDetection = null;
        }
    }

    private void ClearHighlight()
    {
        if (currentTarget != null && targetRenderer != null && originalMaterial != null)
        {
            targetRenderer.material = originalMaterial;
        }

        currentTarget = null;
        currentGrabDetection = null;
        targetRenderer = null;
        originalMaterial = null;
    }
}
