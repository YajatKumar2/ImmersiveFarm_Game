using UnityEngine;

public class GooseTrough : MonoBehaviour
{
    public bool hasFood = false;
    public GameObject visualFood;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //now if the food is already in there at start we have false it
        if(visualFood){
            visualFood.SetActive(false);
        }
    }    
    private void OnTriggerEnter(Collider other){
    //here we need only goose food but not cow, it should not accept cow's
        if(other.CompareTag("GooseFood")){
        Destroy(other.gameObject);

        FillTrough();
        }

    }

    public void FillTrough(){
        hasFood = true;
        Debug.Log("Goose Trough is Filled!");

        if(visualFood) visualFood.SetActive(true);

        }
    
    public void EmptyTrough(){
        hasFood = false;
        Debug.Log("Chicken Trough is Empty! ");

        if(visualFood) visualFood.SetActive(false);
    }


    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
