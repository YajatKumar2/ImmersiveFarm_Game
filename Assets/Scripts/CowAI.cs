using UnityEngine;
using UnityEngine.AI;
using System.Collections; // Required for Coroutines

public class CowAI : MonoBehaviour, IInteractable
{
    public NavMeshAgent agent;
    private bool isWaiting = false; // Flag to prevent constant moving

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomPoint();
    }

    void Update() {
        // If we are moving AND we are close to destination...
        if (!isWaiting && agent.remainingDistance <= agent.stoppingDistance) {
            if (!agent.pathPending) {
                StartCoroutine(WaitAndRoam());
            }
        }
    }

    IEnumerator WaitAndRoam() {
        isWaiting = true; // Tell Update() to stop checking
        
        // Wait for 3 to 6 seconds (Randomly)
        float waitTime = Random.Range(3f, 6f);
        yield return new WaitForSeconds(waitTime);

        MoveToRandomPoint();
        isWaiting = false; // Allow checking again
    }

    void MoveToRandomPoint() {
        Vector3 randomPos = transform.position + Random.insideUnitSphere * 10f;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, 10f, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }

    // Interface Logic
    public string GetPrompt() { return "Pet Cow"; }
    public void OnInteract() {
        Debug.Log("Moo!"); 
        agent.velocity = Vector3.zero; // Stop moving when petted!
    }
}