using UnityEngine;

public class GooseLandShop : MonoBehaviour, IInteractable
{
    [Header("Goose Land Settings")]
    public int cost = 400; // Maybe goose land is cheaper?

    public string GetPrompt() {
        return "Expand Goose Farm ($" + cost + ")";
    }

    public void OnInteract(){
    if (GameManager.Instance.money < cost) {
        Debug.Log("Not enough money for Goose Land!");
        return;
    }

    if (FarmManager.Instance.BuyNewGooseFarm())
    {
        GameManager.Instance.AddMoney(-cost);
        Debug.Log("Goose Land Purchased!");
    }
    else
    {
        Debug.Log("Cannot buy Goose Land – limit reached!");
    }
    }

}