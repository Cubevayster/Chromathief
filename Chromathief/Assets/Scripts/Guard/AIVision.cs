using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AIVision : MonoBehaviour
{
    public Action<Player> OnPlayerSpotted = null;
    public Action<Player> OnPlayerStillInSight = null;
    public Action<Player> OnPlayerLost = null;

    [SerializeField] Player target = null;
    Mesh coneMesh = null;
    [SerializeField] MeshFilter coneMeshFilter = null;
    [SerializeField] MeshRenderer coneMeshRenderer = null;
    [SerializeField, Range(0,180)] int thresholdVision = 45;
    [SerializeField] float distanceVision = 20;

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
        bool _playerSpotted = UpdateConeSight();
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

    public bool UpdateConeSight()
    {
        bool _playerInSight = false;
        int _vertexCount = 2*thresholdVision + 2;
        Vector3[] _vertices = new Vector3[_vertexCount];
        int[] _triangles = new int[(_vertexCount - 1) * 3];
        _vertices[0] = transform.localPosition;
        for (int i = 0; i < _vertexCount - 1; i++)
        {
            Vector3 _directionRaycast = Quaternion.AngleAxis(i- thresholdVision, transform.up) * transform.forward;
            Vector3 _endPoint = transform.position + _directionRaycast * distanceVision;
            RaycastHit _hitData;
            bool _touch = Physics.Raycast(transform.position, _directionRaycast.normalized, out _hitData, distanceVision);
            if (_touch && _hitData.collider == playerCollider) _playerInSight = true;
            _vertices[i + 1] = transform.InverseTransformPoint(_touch ? _hitData.point : _endPoint);
            if (i < _vertexCount - 2)
            {
                _triangles[i * 3] = 0;
                _triangles[i * 3 + 1] = i + 1;
                _triangles[i * 3 + 2] = i + 2;
            }
        }
        coneMesh = new Mesh();
        coneMesh.Clear();
        coneMesh.vertices = _vertices;
        coneMesh.triangles = _triangles;
        coneMesh.RecalculateNormals();
        coneMeshFilter.mesh = coneMesh;

        return _playerInSight;
    }
   
}
