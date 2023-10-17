using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float maxSightDistance = 10f;
    [SerializeField] float maxHearingDistance = 20f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float minIdleTime = 1f;
    [SerializeField] float maxIdleTime = 5f;
    [SerializeField] float minWalkTime = 2f; 
    [SerializeField] float maxWalkTime = 10f; 

    private Vector3 target = default;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private float idleTime;
    private float walkTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        walkTime = Random.Range(minWalkTime, maxWalkTime);
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        EntityManager.EntityManagerInstance.RegisterGuard(this);
    }

    public void MoveTowardsIfHeard(Vector3 noisePosition)
    {
       if(Vector3.Distance(this.gameObject.transform.position, noisePosition) < maxHearingDistance)
        {
            
            target = noisePosition;
        }
    }

    private void Update()
    {
        if (target != default)
        {
            navMeshAgent.speed = runSpeed;
            navMeshAgent.destination = target;
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
            target = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.position == target)
        {
            target = default;
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
