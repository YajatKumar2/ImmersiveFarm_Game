using UnityEngine;

public class GooseLandShop : MonoBehaviour, IInteractable
{
    [Header("Goose Land Settings")]
    public int cost = 400; // Maybe goose land is cheaper?
    public AudioClip sellSound;

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
        if(sellSound) AudioSource.PlayClipAtPoint(sellSound, transform.position);
        Debug.Log("Goose Land Purchased!");
    }
    else
    {
        Debug.Log("Cannot buy Goose Land – limit reached!");
    }
    }

}