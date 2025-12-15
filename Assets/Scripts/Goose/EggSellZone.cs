using UnityEngine;

public class EggSellZone : MonoBehaviour
{

    public AudioClip sellSound;
    //sound here
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Egg")){
            GameManager.Instance.AddMoney(10);
            if(sellSound) AudioSource.PlayClipAtPoint(sellSound, transform.position);

            Destroy(other.gameObject);

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public AudioClip sellSound;
    if(sellSound) AudioSource.PlayClipAtPoint(sellSound, transform.position);

    */
}
