using UnityEngine;
using UnityEngine.AI;

// Note: We added ", IInteractable" here
public class AnimalAI : MonoBehaviour, IInteractable
{
    [Header("AI Settings")]
    public NavMeshAgent agent;
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    private float timer;

    [Header("Status")]
    public string animalName = "Cow"; // Change to "Chicken" on the sphere
    public bool isHungry = false;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    // --- NEW INTERACTION CODE BELOW ---

    public string GetInteractionPrompt()
    {
        // Dynamic Text!
        if(isHungry) return $"Feed {animalName}";
        return $"Pet {animalName}";
    }

    public void OnInteract()
    {
        // Simple feedback for now
        Debug.Log($"You touched the {animalName}!");
        
        // We will add the "Feeding" logic here on Day 3
        // For now, let's just toggle hunger to test the UI
        isHungry = !isHungry; 
    }

    // ----------------------------------

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}