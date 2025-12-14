using UnityEngine;

public class ChickenFarmUnit : MonoBehaviour
{
    [Header("Setup")]
    public Transform[] slots;       // Drag Slot_1, Slot_2, etc here
    public GameObject goosePrefab;  // Drag Goose Prefab (Chicken_Root)
    public GameObject barrelPrefab; // Drag Barrel Prefab (ChickenTrough)

    private int currentCount = 0;

    // The Manager calls this function
    public bool TrySpawnAnimal() {
        if (currentCount >= slots.Length) {
            return false;
        }

        // Get positions
        Transform currentSlot = slots[currentCount];
        Transform goosePos = currentSlot.Find("GoosePos");
        Transform barrelPos = currentSlot.Find("BarrelPos");

        // 1. Spawn the Barrel
        GameObject newBarrel = Instantiate(barrelPrefab, barrelPos.position, barrelPos.rotation);
        
        // 2. Spawn the Goose
        GameObject newGoose = Instantiate(goosePrefab, goosePos.position, goosePos.rotation);

        // 3. Link Brains (Crucial: Use ChickenTrough and ChickenAI types!)
        GooseAI brain = newGoose.GetComponent<GooseAI>();
        if (brain != null) {
            brain.assignedTrough = newBarrel.GetComponent<GooseTrough>();
        }

        currentCount++;
        return true; 
    }

    public bool IsFull() {
        return currentCount >= slots.Length;
    }
}