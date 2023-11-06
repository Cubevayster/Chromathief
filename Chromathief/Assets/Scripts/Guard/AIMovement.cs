using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Action OnDestinationReached = null;
    public Action OnEndWait = null;

    [SerializeField] Animator animator = null;
    [SerializeField] NavMeshAgent agent = null;
    
    [SerializeField] bool canMove = true;
    [SerializeField] float walkingSpeed = 0.3f;
    [SerializeField] float runningSpeed = 2.5f;

    [SerializeField] bool projectDestinationOnNavmesh = true;
    [SerializeField] Vector3 destination = Vector3.zero;

    public float GetWalkingSpeed() => walkingSpeed;
    public float GetRunningSpeed() => runningSpeed;

    void Update()
    {
        SendAgentVelocityToAnimator();
        CheckIsAtDestination(destination);
    }

    private void OnDestroy()
    {
        OnDestinationReached = null;
        OnEndWait = null;
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

    public void ChangeMaxSpeed(ALERT_TYPES _alertType)
    {
        if (!agent) return;
        switch (_alertType)
        {
            case ALERT_TYPES.None:
                agent.speed = walkingSpeed;
                break;

            case ALERT_TYPES.Warning:
                agent.speed = runningSpeed;
                break;
        }
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
        agent.ResetPath();
        agent.SetDestination(destination);
    }

    void CheckIsAtDestination(Vector3 _destination, float _distanceThreshold = 0.01f)
    {
        if (!agent.hasPath) return;
        bool _isAtDestination = false;
        if (projectDestinationOnNavmesh && agent) _isAtDestination = agent.remainingDistance <= agent.stoppingDistance;
        else _isAtDestination = (_destination - transform.position).magnitude <= _distanceThreshold;
        if (_isAtDestination) OnDestinationReached?.Invoke();
    }

    public void StopMovement()
    {
        if (!agent) return;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }

    public IEnumerator StopMovement(float _timeWhereStopped)
    {
        if (!agent) yield break;
        StopMovement();
        yield return new WaitForSeconds(_timeWhereStopped);
        agent.isStopped = false;
        OnEndWait?.Invoke();
    }
}
