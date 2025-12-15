using UnityEngine;

public class LandShop : MonoBehaviour, IInteractable
{
    [Header("Cow Land Settings")]
    public int cost = 500;
    public AudioClip sellSound;

    public string GetPrompt() {
        return "Expand Cow Farm ($" + cost + ")";
    }

    public void OnInteract(){
    if (GameManager.Instance.money < cost) {
        Debug.Log("Not enough money for Cow Land!");
        return;
    }

    if (FarmManager.Instance.BuyNewCowFarm())
    {
        GameManager.Instance.AddMoney(-cost);
        if(sellSound) AudioSource.PlayClipAtPoint(sellSound, transform.position);
        Debug.Log("Cow Land Purchased!");
    }
    else
    {
        Debug.Log("Cannot buy Cow Land, limit reached!");
    }
    }

}