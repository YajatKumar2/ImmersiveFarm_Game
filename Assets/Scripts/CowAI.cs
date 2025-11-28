using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables; // Needed for Timeline
using UnityEngine.UI;        // <--- NEEDED FOR UI (Hunger Bar)
using System.Collections;

public class CowAI : MonoBehaviour, IInteractable
{
    [Header("Components")]
    public NavMeshAgent agent;
    public Trough assignedTrough;
    
    [Header("UI Settings")]
    public Image hungerBar; // <--- The missing variable!
    
    [Header("Milking Settings")]
    public GameObject milkPrefab;           
    public PlayableDirector timelineDirector; 
    
    // STATES
    private bool isHungry = false;
    private bool isEating = false;
    private bool isReadyToMilk = false; 

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        
        // Start the logic loops
        StartCoroutine(WanderRoutine());
        StartCoroutine(HungerTimer());
    }

    void Update() {
        if (isHungry && !isEating && !isReadyToMilk && assignedTrough.hasFood) {
            MoveToFood();
        }
    }

    void MoveToFood() {
        agent.SetDestination(assignedTrough.transform.position);

        if (Vector3.Distance(transform.position, assignedTrough.transform.position) < 2.5f) {
            StartCoroutine(EatRoutine());
        }
    }

    IEnumerator EatRoutine() {
        isEating = true;
        agent.isStopped = true; 
        Debug.Log("Cow is Eating...");

        yield return new WaitForSeconds(3f); 

        assignedTrough.EmptyTrough(); 
        
        // SWITCH STATE: Ready for Milking
        isEating = false;
        isHungry = false;
        isReadyToMilk = true; 
        
        Debug.Log("Cow is Full & Ready to Milk!");
    }

    IEnumerator MilkingAction() {
        // 1. Play Timeline
        if (timelineDirector) {
            timelineDirector.Play();
            yield return new WaitForSeconds((float)timelineDirector.duration);
        }
        else {
            yield return new WaitForSeconds(2f);
        }

        // 2. Spawn Milk
        Instantiate(milkPrefab, transform.position + (transform.right * 0.7f) + Vector3.up, Quaternion.identity);

        // 3. Reset
        isReadyToMilk = false;
        agent.isStopped = false; 
        
        StartCoroutine(WanderRoutine());
        StartCoroutine(HungerTimer());
    }

    // --- INTERACTION ---
    public string GetPrompt() { 
        if (isHungry) return "Cow is Hungry! (Go to Silo and Fill Trough)";
        if (isReadyToMilk) return "Press E to Milk Cow"; 
        return "Pet Cow"; 
    }

    public void OnInteract() { 
        if (isReadyToMilk) {
            StartCoroutine(MilkingAction());
        }
        else {
            Debug.Log("Moo!"); 
        }
    }

    // --- WANDER LOGIC ---
    IEnumerator WanderRoutine() {
        while (!isHungry && !isReadyToMilk) {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * 4f;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPos, out hit, 4f, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
            yield return new WaitForSeconds(5f);
        }
    }

    // --- HUNGER CLOCK & UI UPDATE ---
    IEnumerator HungerTimer() {
        float timeToHungry = 30f; 
        float timer = 0f;

        // Reset Bar to Green
        if(hungerBar) {
            hungerBar.color = Color.green;
            hungerBar.fillAmount = 1f;
        }

        // Count up
        while (timer < timeToHungry) {
            timer += Time.deltaTime;
            
            // Drain the bar
            if(hungerBar) {
                hungerBar.fillAmount = 1f - (timer / timeToHungry);
            }
            
            yield return null; 
        }

        // Finished
        isHungry = true;
        agent.ResetPath();
        
        // Turn Red
        if(hungerBar) {
            hungerBar.color = Color.red;
            hungerBar.fillAmount = 1f; 
        }
    }
}