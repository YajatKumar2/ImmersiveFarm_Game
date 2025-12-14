using UnityEngine;

public class GooseShop : MonoBehaviour, IInteractable
{
    // You can rename this class to 'ChickenShop' if your file is named ChickenShop.cs
    // MAKE SURE the class name matches the file name!

    [Header("Shop Settings")]
    public int cost = 30; // Price of one goose

    public string GetPrompt() {
        return "Buy Goose ($" + cost + ")";
    }

    public void OnInteract() {
        // 1. Check Money
        if (GameManager.Instance.money >= cost) {
            
            // 2. Ask the Manager to find a spot
            bool success = FarmManager.Instance.TryBuyGoose();

            if (success) {
                // 3. Only pay if the Manager found a spot
                GameManager.Instance.AddMoney(-cost);
                Debug.Log("Goose Purchased!");
            }
            else {
                Debug.Log("No space! Buy more Goose Land.");
            }
        } 
        else {
            Debug.Log("Not enough cash!");
        }
    }
}