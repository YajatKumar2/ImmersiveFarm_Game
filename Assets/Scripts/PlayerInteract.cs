using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float range = 3f;
    public LayerMask interactMask; 
    public TextMeshProUGUI promptText; // Drag UI Text here later

    void Update() {
        // Create a ray from the center of the camera shooting forward
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // If the ray hits something...
        if (Physics.Raycast(ray, out hit, range, interactMask)) {
            
            // Check if that thing has our Interface
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null) {
                // If we have a text box, show the prompt
                if(promptText) promptText.text = interactable.GetPrompt();
                
                // If we press E, do the action
                if (Input.GetKeyDown(KeyCode.E)) {
                    interactable.OnInteract();
                }
            } else {
                if(promptText) promptText.text = "";
            }
        }
        else {
            if(promptText) promptText.text = "";
        }
    }
}