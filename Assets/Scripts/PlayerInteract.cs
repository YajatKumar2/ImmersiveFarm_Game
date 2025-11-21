using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [Header("Settings")]
    public float interactRange = 3f;
    [SerializeField] private LayerMask interactLayers; // We will set this in Inspector

    [Header("UI References")]
    public TextMeshProUGUI promptText;

    private void Update()
    {
        // Create a ray from the center of the camera
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Shoot the laser
        // Note: We use 'interactLayers' to only hit things we care about (optimization)
        if (Physics.Raycast(ray, out hit, interactRange, interactLayers))
        {
            // Check if the object we hit has the 'IInteractable' interface
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                // Show the text!
                promptText.text = interactable.GetInteractionPrompt();
                promptText.gameObject.SetActive(true);

                // Did we press E?
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.OnInteract();
                }
            }
            else
            {
                // Hit something, but it's not interactable (like a wall)
                promptText.gameObject.SetActive(false);
            }
        }
        else
        {
            // Hit nothing (looking at sky)
            promptText.gameObject.SetActive(false);
        }
    }
}