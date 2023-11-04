using System;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFollower : MonoBehaviour
{
    public Action<Vector3> OnPathDestinationAcquired = null;

    [SerializeField] bool isCyclicFollowing = false;
    [SerializeField] int pathIndex = -1;
    [SerializeField] int pointsIndex = 0;
    [SerializeField] List<Vector3> points = new List<Vector3>();

    [SerializeField] Vector3 currentTargetPosition = Vector3.zero;
    [SerializeField] bool ascendingOrder = true;

    void Start() => GetPathToFollow();

    private void OnDestroy()
    {
        OnPathDestinationAcquired = null;
    }

    void GetPathToFollow()
    {
        if (pathIndex < 0) return;
        PME_PathManager _pathManager = PME_PathManager.Instance;
        if (!_pathManager) return;
        points = _pathManager.GetPath(pathIndex).GetPathPoints();
        GetNextPosition();
    }

    void GetNextIndex()
    {
        pointsIndex += isCyclicFollowing || ascendingOrder ? 1 : -1;
        if (isCyclicFollowing && pointsIndex >= points.Count) pointsIndex = 0;
        else if (!isCyclicFollowing)
        {
            if(ascendingOrder && pointsIndex >= points.Count)
            {
                pointsIndex = points.Count - 2;
                ascendingOrder = false;
            }
            else if (!ascendingOrder && pointsIndex < 0)
            {
                pointsIndex = 1;
                ascendingOrder = true;
            }
        }
    }

    public void GetNextPosition()
    {
        if (pointsIndex >= points.Count) return;
        currentTargetPosition = points[pointsIndex];
        OnPathDestinationAcquired.Invoke(currentTargetPosition);
        GetNextIndex();
    }
}
