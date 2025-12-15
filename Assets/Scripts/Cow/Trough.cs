using UnityEngine;

public class Trough : MonoBehaviour
{
    public bool hasFood = false; 
    public GameObject visualFood; 

    void Start() {
        if (visualFood) visualFood.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        // Check if the object is the FeedBag (tagged "Pickup")
        if (other.CompareTag("Pickup")) {
            
            // Destroy the Bag
            Destroy(other.gameObject);
            
            // Fill the Trough
            FillTrough();
        }
    }

    public void FillTrough() {
        hasFood = true;
        Debug.Log("Trough is FULL!");
        
        if (visualFood) visualFood.SetActive(true);
    }

    public void EmptyTrough() {
        hasFood = false;
        Debug.Log("Trough is EMPTY.");
        
        if (visualFood) visualFood.SetActive(false);
    }
}