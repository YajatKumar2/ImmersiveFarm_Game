using UnityEngine;

public class CowShop : MonoBehaviour, IInteractable
{
    [Header("Shop Settings")]
    public GameObject cowPrefab;   // The Blueprint (Your Cow_Root Prefab)
    public Transform spawnPoint;   // Where to drop it
    public Trough barnTrough;      // The Trough the new cow should use
    public int cost = 100;

    public string GetPrompt() {
        return "Press E to Buy Cow ($" + cost + ")";
    }

    public void OnInteract() {
        // 1. Check if we have enough money
        if (GameManager.Instance.money >= cost) {
            BuyCow();
        } 
        else {
            Debug.Log("Not enough cash! You need $" + cost);
        }
    }

    void BuyCow() {
        // 2. Pay the price
        GameManager.Instance.AddMoney(-cost);
        
        // 3. Create the Cow
        GameObject newCow = Instantiate(cowPrefab, spawnPoint.position, spawnPoint.rotation);
        
        // 4. CONNECT THE WIRES (Crucial)
        // The prefab doesn't know where the trough is. We must tell it now.
        CowAI newBrain = newCow.GetComponent<CowAI>();
        if (newBrain != null) {
            newBrain.assignedTrough = barnTrough;
        }

        Debug.Log("Cow Purchased! You now have a herd.");
    }
}