using UnityEngine;

public class GooseLandShop : MonoBehaviour, IInteractable
{
    [Header("Goose Land Settings")]
    public int cost = 400; // Maybe goose land is cheaper?

    public string GetPrompt() {
        return "Expand Goose Farm ($" + cost + ")";
    }

    public void OnInteract() {
        if (GameManager.Instance.money >= cost) {
            
            // 1. Pay Money
            GameManager.Instance.AddMoney(-cost);
            
            // 2. SPECIFICALLY buy a Goose Farm
            FarmManager.Instance.BuyNewGooseFarm(); 
            
            Debug.Log("Goose Land Purchased!");
        }
        else {
            Debug.Log("Not enough money for Goose Land!");
        }
    }
}