using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float maxSightDistance = 10f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float minIdleTime = 1f;
    [SerializeField] float maxIdleTime = 5f;
    [SerializeField] float minWalkTime = 2f; // Minimum time to walk between idle states.
    [SerializeField] float maxWalkTime = 10f; // Maximum time to walk between idle states.

    private Transform target;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private float idleTime;
    private float walkTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // Initialize walkTime and idleTime at the start.
        walkTime = Random.Range(minWalkTime, maxWalkTime);
        idleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    private void Update()
    {
        if (target != null)
        {
            navMeshAgent.speed = runSpeed;
            navMeshAgent.destination = target.position;
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            walkTime = maxWalkTime;
            idleTime = maxIdleTime;
        }
        else
        {
           
            if (walkTime <= 0)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
                idleTime = maxIdleTime;
            }
            else if (idleTime <= 0)
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", false);
                Vector3 randomPoint = RandomNavmeshLocation(maxSightDistance);
                navMeshAgent.speed = walkSpeed;
                navMeshAgent.destination = randomPoint;
                walkTime = maxWalkTime;
            }
            else
            {
                walkTime -= Time.deltaTime;
                idleTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            target = null;
        }
    }

    private Vector3 RandomNavmeshLocation(float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, distance, UnityEngine.AI.NavMesh.AllAreas);
        return hit.position;
    }
}
