using UnityEngine;
using System.Collections.Generic;

public class FarmManager : MonoBehaviour
{
    public static FarmManager Instance;

    // ================== COW SETTINGS ==================
    [Header("Cow Settings")]
    public GameObject cowFarmPrefab;    
    public Transform cowStartOrigin;    
    public float farmWidth = 20f;    
    public int maxFarms = 3;


    private List<FarmUnit> activeCowFarms = new List<FarmUnit>();

    // ================== GOOSE SETTINGS (NEW) ==================
    [Header("Goose Settings")]
    public GameObject gooseFarmPrefab;      // Drag ChickenFarm_Template here
    public Transform gooseStartOrigin;      // Where the first goose pen spawns
    
    public int maxGooseFarms = 3;     

    private List<ChickenFarmUnit> activeGooseFarms = new List<ChickenFarmUnit>();

    void Awake() {
        Instance = this;
    }

    void Start() {
        // Spawn the first farms automatically
        SpawnNewCowFarm();
        SpawnNewGooseFarm(); // <--- NEW: Spawn the first goose pen!
    }

    // ================== COW LOGIC ==================
    public bool TryBuyCow() {
        foreach (FarmUnit farm in activeCowFarms) {
            if (!farm.IsFull()) {
                farm.TrySpawnAnimal();
                return true; 
            }
        }
        Debug.Log("All cow farms full!");
        return false; 
    }

    public bool BuyNewCowFarm() {
        if (activeCowFarms.Count >= maxFarms){
            Debug.Log("Max cow farms reached!"); 
            return false;
        }
        SpawnNewCowFarm();
        return true;
    }

    void SpawnNewCowFarm() {
        Vector3 spawnPos = cowStartOrigin.position + (Vector3.left * (activeCowFarms.Count * farmWidth));
        GameObject newFarm = Instantiate(cowFarmPrefab, spawnPos, Quaternion.identity);
        activeCowFarms.Add(newFarm.GetComponent<FarmUnit>());
    }

    // ================== GOOSE LOGIC (NEW) ==================
    
    // 1. Called by ChickenShop.cs when you press "Buy Goose"
    public bool TryBuyGoose() {
        // Check every goose farm we own
        foreach (ChickenFarmUnit farm in activeGooseFarms) {
            // Is there room?
            if (!farm.IsFull()) {
                // Yes! Spawn a goose there.
                farm.TrySpawnAnimal();
                return true; // Success
            }
        }
        // If we loop through all and find no room:
        Debug.Log("All goose farms full!");
        return false; // Fail
    }

    // 2. Called by a future "Buy Goose Land" button
    public bool BuyNewGooseFarm() {
        // You can add a max limit check here if you want (like Cows)
        if (activeGooseFarms.Count >= maxGooseFarms){
            Debug.Log("Max Goose farms reached!"); 
            return false;
        }
        SpawnNewGooseFarm();
        return true;
        
    }

    // 3. The math to place the farm
    void SpawnNewGooseFarm() {
        // Calculate position based on how many we already have
        // NOTE: We use the same 'farmWidth', or you can add a separate 'gooseFarmWidth' variable
        Vector3 spawnPos = gooseStartOrigin.position + (Vector3.forward * (activeGooseFarms.Count * farmWidth));
        
        GameObject newFarm = Instantiate(gooseFarmPrefab, spawnPos, Quaternion.identity);
        
        // Add to the GOOSE list (not the Cow list!)
        activeGooseFarms.Add(newFarm.GetComponent<ChickenFarmUnit>());
        
        Debug.Log("New Goose Farm Spawned!");
    }
}