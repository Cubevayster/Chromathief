using System;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Action<Vector3> OnDestinationReached = null;

    [SerializeField] Animator animator = null;
    [SerializeField] NavMeshAgent agent = null;
    
    [SerializeField] bool canMove = true;
    [SerializeField] float walkingSpeed = 1;
    [SerializeField] float runningSpeed = 5;

    [SerializeField] bool projectDestinationOnNavmesh = true;
    [SerializeField] Vector3 destination = Vector3.zero;

    private void Awake()
    {
        //OnDestinationReached += (Vector3 _destination) => StopMovement();
    }

    void Update()
    {
        SendAgentVelocityToAnimator();
        CheckIsAtDestination(destination);
    }

    private void OnDestroy()
    {
        OnDestinationReached = null;
    }

    void SendFloatAnimator(string _key, float _value)
    {
        if (!animator) return;
        animator.SetFloat(_key, _value);
    }

    void SendAgentVelocityToAnimator()
    {
        if (!agent) return;
        SendFloatAnimator("Velocity", agent.velocity.magnitude);
    }

    Vector3 ProjectDestinationOnNavmesh(Vector3 _destination)
    {
        if (!projectDestinationOnNavmesh) return _destination;
        NavMeshHit _hit;
        if (NavMesh.SamplePosition(_destination, out _hit, agent.height * 2, NavMesh.AllAreas))
            return _hit.position;
        return _destination;
    }

    public void MoveToDestination(Vector3 _destination)
    {
        if (!canMove) return;
        if (projectDestinationOnNavmesh) destination = ProjectDestinationOnNavmesh(_destination);
        else destination = _destination;
        agent.SetDestination(destination);
    }

    void CheckIsAtDestination(Vector3 _destination, float _distanceThreshold = 0.01f)
    {
        bool _isAtDestination = false;
        if (projectDestinationOnNavmesh && agent) _isAtDestination = agent.remainingDistance <= agent.stoppingDistance;
        else _isAtDestination = (_destination - transform.position).magnitude <= _distanceThreshold;
        if (_isAtDestination) OnDestinationReached?.Invoke(_destination);
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }
}
