using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CowAI : MonoBehaviour, IInteractable
{
    public NavMeshAgent agent;
    public Trough assignedTrough; // <-- CRITICAL: We must drag the trough here
    
    // STATES
    private bool isHungry = false;
    private bool isEating = false;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        
        // Start the logic loops
        StartCoroutine(WanderRoutine());
        StartCoroutine(HungerTimer());
    }

    // This runs every frame to check logic
    void Update() {
        // Logic: If hungry AND not eating AND trough has food...
        if (isHungry && !isEating && assignedTrough.hasFood) {
            MoveToFood();
        }
    }

    void MoveToFood() {
        agent.SetDestination(assignedTrough.transform.position);

        // Check if close enough to eat (2 meters)
        float dist = Vector3.Distance(transform.position, assignedTrough.transform.position);
        if (dist < 2.0f) {
            StartCoroutine(Eat());
        }
    }

    IEnumerator Eat() {
        isEating = true;
        agent.isStopped = true; // Freeze movement
        Debug.Log("Cow is Eating...");

        yield return new WaitForSeconds(3f); // Chew for 3 seconds

        // Finish Eating
        assignedTrough.EmptyTrough(); // Tell the Trough it's empty
        isHungry = false;
        isEating = false;
        agent.isStopped = false; // Unfreeze
        
        Debug.Log("Cow is Full!");
        
        // Restart the wandering logic
        StartCoroutine(WanderRoutine());
        StartCoroutine(HungerTimer());
    }

    // --- WANDER LOGIC (Same as before) ---
    IEnumerator WanderRoutine() {
        while (!isHungry) {
            // Pick random point
            Vector3 randomPos = transform.position + Random.insideUnitSphere * 4f;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPos, out hit, 4f, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
            
            // Wait 5 seconds before moving again
            yield return new WaitForSeconds(5f);
        }
    }

    // --- HUNGER CLOCK ---
    IEnumerator HungerTimer() {
        // Wait 15 seconds, then get hungry
        yield return new WaitForSeconds(15f);
        isHungry = true;
        agent.ResetPath(); // Stop wandering immediately
        Debug.Log("Cow is HUNGRY!");
    }

    // --- INTERACTION ---
    public string GetPrompt() { 
        if (isHungry) return "Cow is Hungry! (Fill Trough)";
        return "Pet Cow"; 
    }

    public void OnInteract() { 
        Debug.Log("Mooo!"); 
    }
}