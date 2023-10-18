using System;
using UnityEngine;

public class AIVision : MonoBehaviour
{
    public Action<Player> OnPlayerSpotted = null;
    public Action<Player> OnPlayerStillInSight = null;
    public Action<Player> OnPlayerLost = null;

    [SerializeField] Player target = null;
    [SerializeField] float thresholdVision = 90;

    Collider playerCollider = null;
    bool playerIsAlreadySpotted = false;

    private void Awake()
    {
        OnPlayerSpotted += (Player _target) => RegisterPlayerSpotted(true);
        OnPlayerLost += (Player _target) => RegisterPlayerSpotted(false);
    }

    void Start() => GetComponents();

    void Update() => UpdateVision();

    private void OnDestroy()
    {
        OnPlayerSpotted = null;
        OnPlayerStillInSight = null;
        OnPlayerLost = null;
    }

    void GetComponents()
    {
        if (!target) return;
        playerCollider = target.GetComponent<Collider>();
    }

    void UpdateVision()
    {
        bool _playerSpotted = CheckTargetVisible();
        if (!playerIsAlreadySpotted && _playerSpotted) OnPlayerSpotted.Invoke(target);
        else if (playerIsAlreadySpotted && _playerSpotted) OnPlayerStillInSight.Invoke(target);
        else if (playerIsAlreadySpotted && !_playerSpotted) OnPlayerLost.Invoke(target);
    }

    void RegisterPlayerSpotted(bool _playerSpotted)
    {
        if (_playerSpotted) target.AddDetected(gameObject);
        else target.RemoveDetected(gameObject);
        playerIsAlreadySpotted = _playerSpotted;
    }

    bool IsTargetInSight()
    {
        if (!playerCollider) return false;
        Vector3 _AIToPlayer = playerCollider.transform.position - transform.position;
        _AIToPlayer = Vector3.ProjectOnPlane(_AIToPlayer, transform.up).normalized;
        Vector3 _forwardAI = transform.forward;
        float _dot = Vector3.Dot(_AIToPlayer, _forwardAI);
        return _dot >= Mathf.Cos(Mathf.Deg2Rad * thresholdVision);
    }

    bool CheckTargetVisible()
    {
        if (!playerCollider || !IsTargetInSight()) return false;
        RaycastHit _hitData;
        if (!Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out _hitData)) return false;
        return _hitData.collider == playerCollider;
    }
}
