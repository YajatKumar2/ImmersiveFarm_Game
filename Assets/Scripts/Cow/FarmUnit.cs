using UnityEngine;

public class FarmUnit : MonoBehaviour
{
    [Header("Setup")]
    public Transform[] slots;       
    public GameObject animalPrefab; 
    public GameObject troughPrefab; 

    private int currentCount = 0;

    // The Manager calls this function
    public bool TrySpawnAnimal() {
        
        if (currentCount >= slots.Length) {
            return false;
        }

        // 2. Get the specific positions from the current slot
        Transform currentSlot = slots[currentCount];
        Transform cowPos = currentSlot.Find("CowPos");
        Transform troughPos = currentSlot.Find("TroughPos");

        // 3. Spawn the Trough (It should have NavMeshObstacle to carve the hole)
        GameObject newTrough = Instantiate(troughPrefab, troughPos.position, troughPos.rotation);

        // 4. Spawn the Cow
        GameObject newCow = Instantiate(animalPrefab, cowPos.position, cowPos.rotation);

        // 5. Link Brains (Tell the Cow: "This is YOUR trough")
        CowAI brain = newCow.GetComponent<CowAI>();
        if (brain != null) {
            brain.assignedTrough = newTrough.GetComponent<Trough>();
        }

        // 6. Increase count so next cow goes to next slot
        currentCount++;
        return true; 
    }

    public bool IsFull() {
        return currentCount >= slots.Length;
    }
}