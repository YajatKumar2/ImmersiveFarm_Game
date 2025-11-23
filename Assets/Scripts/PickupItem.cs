using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickupItem : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    private Collider col; // Reference to the Collider
    private Transform holdPoint;
    private bool isHeld = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>(); // Get the collider
        
        if (Camera.main != null)
        {
            holdPoint = Camera.main.transform.Find("HoldPoint");
        }
    }

    public string GetPrompt()
    {
        // We return "" (Empty) when held, so the text doesn't annoy you
        return isHeld ? "" : "Press E to Pick Up";
    }

    private void Update()
    {
        // This listens for E regardless of where you look
        if (isHeld)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Drop Button Pressed!"); // Check Console for this
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

        // 1. Disable Physics
        rb.isKinematic = true;
        rb.useGravity = false;
        
        // 2. DISABLE COLLIDER (Fixes the jitter/glitch)
        if(col) col.enabled = false;

        // 3. Parent and Snap
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isHeld = true;
        Debug.Log("Picked Up");
    }

    private void Drop()
    {
        // 1. Unparent
        transform.SetParent(null);

        // 2. Enable Physics
        rb.isKinematic = false;
        rb.useGravity = true;

        // 3. RE-ENABLE COLLIDER
        if(col) col.enabled = true;

        // 4. Throw
        rb.AddForce(Camera.main.transform.forward * 5f, ForceMode.Impulse);

        isHeld = false;
        Debug.Log("Dropped");
    }
}