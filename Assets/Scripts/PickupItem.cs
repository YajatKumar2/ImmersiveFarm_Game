using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickupItem : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    private Collider col;
    private Transform holdPoint;
    private bool isHeld = false;
    
    // BUG FIX: Prevent instant dropping
    private float pickupTime; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        
        if (Camera.main != null)
        {
            holdPoint = Camera.main.transform.Find("HoldPoint");
        }
    }

    public string GetPrompt()
    {
        return isHeld ? "" : "Press E to Pick Up";
    }

    private void Update()
    {
        // Only allow dropping if 0.5 seconds have passed since pickup
        if (isHeld && Time.time > pickupTime + 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Drop();
            }
        }
    }

    public void OnInteract()
    {
        if (!isHeld) 
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        if (holdPoint == null) return;

        // Set the timer so we don't drop instantly
        pickupTime = Time.time;

        rb.isKinematic = true;
        rb.useGravity = false;
        
        // Disable collider so it doesn't hit the player
        if(col) col.enabled = false;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isHeld = true;
    }

    private void Drop()
    {
        transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;

        // Re-enable collider
        if(col) col.enabled = true;

        // Throw force
        rb.AddForce(Camera.main.transform.forward * 5f, ForceMode.Impulse);

        isHeld = false;
    }
}