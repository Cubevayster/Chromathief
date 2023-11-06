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

    AIVision visionComponent = null;

    private Vector3 target = default;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private float idleTime;
    private float walkTime;

    private void Awake() => GetComponents();

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        walkTime = Random.Range(minWalkTime, maxWalkTime);
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        //EntityManager.EntityManagerInstance.RegisterGuard(this);
    }

    void GetComponents()
    {
        visionComponent = GetComponent<AIVision>();
        if (!visionComponent) return;
        visionComponent.OnPlayerStillInSight += (Player _target) => { target = _target.transform.position; };
        visionComponent.OnPlayerLost += (Player _target) => { target = default; } ;
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
        
    }

    private void OnTriggerExit(Collider other)
    {
        
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
