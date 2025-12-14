using UnityEngine;

public class LandShop : MonoBehaviour, IInteractable
{
    [Header("Cow Land Settings")]
    public int cost = 500;

    public string GetPrompt() {
        return "Expand Cow Farm ($" + cost + ")";
    }

    public void OnInteract() {
        if (GameManager.Instance.money >= cost) {
            
            // 1. Pay Money
            GameManager.Instance.AddMoney(-cost);
            
            // 2. SPECIFICALLY buy a Cow Farm
            FarmManager.Instance.BuyNewCowFarm(); // <--- FIX IS HERE
            
            Debug.Log("Cow Land Purchased!");
        }
        else {
            Debug.Log("Not enough money for Cow Land!");
        }
    }
}