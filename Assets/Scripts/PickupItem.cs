using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Auto-adds Rigidbody if missing
public class PickupItem : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    private Transform holdPoint;
    private bool isHeld = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        // Find the HoldPoint we created earlier on the Camera
        if (Camera.main != null)
        {
            holdPoint = Camera.main.transform.Find("HoldPoint");
        }
    }

    public string GetInteractionPrompt()
    {
        // Changing text based on state
        return isHeld ? "Press E to Drop" : "Press E to Pick Up";
    }

    public void OnInteract()
    {
        if (isHeld) Drop();
        else Pickup();
    }

    private void Pickup()
    {
        if (holdPoint == null) 
        {
            Debug.LogError("Cannot find HoldPoint! Did you name it exactly 'HoldPoint'?");
            return;
        }

        // 1. Disable Physics so it doesn't fall
        rb.isKinematic = true; 
        rb.useGravity = false;

        // 2. Parent it to the hand
        transform.SetParent(holdPoint);
        
        // 3. Snap to position (Reset local coordinates)
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isHeld = true;
    }

    private void Drop()
    {
        // 1. Un-parent
        transform.SetParent(null);

        // 2. Re-enable Physics
        rb.isKinematic = false;
        rb.useGravity = true;

        // 3. Add a little throw force
        rb.AddForce(Camera.main.transform.forward * 5f, ForceMode.Impulse);

        isHeld = false;
    }
}