using UnityEngine;
using UnityEngine.UI;

public class DistanceInteractable : MonoBehaviour
{
    public float interactionDistance = 2f;
    public LayerMask interactableLayer;
    public GameObject interactableText;
    public Image cursorImage;
    public float brightenFactor = 2f;
    public float defaultBrightness = 0.4f;

    private void Update()
    {
        // Cast a ray from the camera to detect interactable objects
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // If an interactable object is hit within the interaction distance
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                // Show interactable text
                interactableText.SetActive(true);

                // Calculate brightness based on distance
                float distance = Vector3.Distance(hit.point, ray.origin);
                float brightness = Mathf.Lerp(defaultBrightness, 1f, (interactionDistance - distance) / interactionDistance);
                cursorImage.color = new Color(1f, 1f, 1f, brightness);

                // Check for input to interact with the object
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactableObject.Interact();
                }
            }
        }
        else
        {
            // If no interactable object is hit or the object is out of range
            interactableText.SetActive(false);
            cursorImage.color = new Color(1f, 1f, 1f, defaultBrightness);
        }
    }
}