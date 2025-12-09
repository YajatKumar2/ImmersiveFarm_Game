using UnityEngine;

public class GooseShop : MonoBehaviour, IInteractable
{
    [Header("Shop Settings")]
    public GameObject goosePrefab;

    public Transform spawnPoint;

    public GooseTrough coopTrough;

    public AudioClip sellSound;

    public int cost = 30;

    public string GetPrompt(){
        return "Buy Goose ($" + cost + ")";
    }

    public void OnInteract(){
        if(GameManager.Instance.money >= cost){
            BuyBird();
        }
        else{
            Debug.Log("Not enough cash!");
        }
    }

    void BuyBird(){
        GameManager.Instance.AddMoney(-cost);

        GameObject newBird = Instantiate(goosePrefab, spawnPoint.position, spawnPoint.rotation);

        GooseAI brain = newBird.GetComponent<GooseAI>();
        if (brain != null){
            brain.assignedTrough = coopTrough;
        }
        if(sellSound) AudioSource.PlayClipAtPoint(sellSound, transform.position);

        Debug.Log("Goose Purchased!");
    }

    /*// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
