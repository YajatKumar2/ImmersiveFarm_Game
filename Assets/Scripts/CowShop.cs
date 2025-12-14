using UnityEngine;

public class CowShop : MonoBehaviour, IInteractable
{
    [Header("Shop Settings")]
    public GameObject cowPrefab;   // The Blueprint (Your Cow_Root Prefab)
    public Transform spawnPoint;   // Where to drop it
    public Trough barnTrough;      // The Trough the new cow should use
    public AudioClip sellSound;
    
    public int cost = 100;

    public string GetPrompt() {
        return "Press E to Buy Cow ($" + cost + ")";
    }

    public void OnInteract() {
        // 1. Check if we have money
        if (GameManager.Instance.money >= cost) {
            
            // 2. Ask Manager: "Can I please put a cow somewhere?"
            bool success = FarmManager.Instance.TryBuyCow();

            if (success) {
                // 3. Only take money if the Manager said YES
                GameManager.Instance.AddMoney(-cost);
                if(sellSound) AudioSource.PlayClipAtPoint(sellSound, transform.position);
                
                Debug.Log("Cow Purchased!");
            }
            else {
                // Manager said NO (Farms are full)
                Debug.Log("Not enough space!");
            }
        } 
        else {
            Debug.Log("Not enough money!");
        }
    }

    void BuyCow() {
        // 2. Pay the price
        GameManager.Instance.AddMoney(-cost);
        
        AudioSource audio = GetComponent<AudioSource>();
        if(audio) audio.Play(); //This plays whatever audio is in the filee 

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