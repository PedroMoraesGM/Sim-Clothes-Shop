using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionRadius = 2f;

    private InteractableObject currentInteractable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entered collider is an interactable object
        if (((1 << other.gameObject.layer) & interactableLayer) != 0)
        {
            InteractableObject interactableObject = other.GetComponentInParent<InteractableObject>();
            if (interactableObject != null)
            {
                currentInteractable = interactableObject;
                currentInteractable.Select();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exited collider is the current interactable object
        if (currentInteractable != null && currentInteractable.gameObject == other.gameObject)
        {
            currentInteractable.Unselect();
            currentInteractable = null;
        }
    }

    private void Update()
    {
        // Read interaction input
        if (ControllerHelper.Instance.Input.Gameplay.Interact.triggered)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }
}
