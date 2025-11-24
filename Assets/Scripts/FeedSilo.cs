using UnityEngine;

public class FeedSilo : MonoBehaviour, IInteractable
{
    public GameObject feedPrefab; // The blue cube
    public Transform spawnPoint;  // The empty spot in front

    public string GetPrompt() {
        return "Press E to Get Feed";
    }

    public void OnInteract() {
        // Spawn the bag at the spawn point
        Instantiate(feedPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}