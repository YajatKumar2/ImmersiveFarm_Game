using UnityEngine;

public class SellZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        // Check if the object is Milk
        if (other.CompareTag("Milk")) {
            
            // 1. Add Money to Bank ($50)
            GameManager.Instance.AddMoney(50);
            
            // 2. Play Sound (Optional, add later)
            Debug.Log("Sold Milk! +$50");

            // 3. Destroy the Milk Can
            Destroy(other.gameObject);
        }
    }
}