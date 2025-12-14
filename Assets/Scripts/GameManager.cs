using UnityEngine;
using TMPro; // Needed for UI

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Allows other scripts to find this easily

    public int money = 6000;
    public TextMeshProUGUI moneyText; // Drag your UI Text here

    void Awake() {
        // Singleton pattern: There can be only one Bank
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount) {
        money += amount;
        UpdateUI();
    }

    void UpdateUI() {
        if (moneyText) {
            moneyText.text = "$" + money.ToString();
        }
    }
}