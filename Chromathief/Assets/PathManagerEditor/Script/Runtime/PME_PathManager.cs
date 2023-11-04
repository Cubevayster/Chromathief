using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PME_PathManager : Singleton<PME_PathManager>
{
    [SerializeField,Header("List of path :")]List<PME_Path> allPaths = new List<PME_Path>();
    [SerializeField] int currentPathIndex = -1;
    public int CurrentPathIndex => currentPathIndex;
    public bool EmptyPath => allPaths.Count == 0;

    public PME_Path GetPath(int _i)
    {
        if (_i >= allPaths.Count) return new PME_Path();
        return allPaths[_i];
    }

    public void AddPath()
    {
        allPaths.Add(new PME_Path());
        SetCurrentEdit(-1);
        RefreshID();
    }

    public void AddPointAt(int _pathIndex) => allPaths[_pathIndex].AddPoint();

    public void RemovePathPointAt(int _pathindex, int _index) => allPaths[_pathindex].RemovePointAt(_index);
    public void RemoveAllPathPoints(int _pathindex) => allPaths[_pathindex].RemoveAllPoints();

    public void RemoveAt(int _id) {
        allPaths.RemoveAt(_id);
        SetCurrentEdit(-1);
        RefreshID();
    }

    public bool IsEditMatch(int _i) => _i == currentPathIndex;

    void RefreshID()
    {
        for (int i = 0; i < allPaths.Count; i++)
            allPaths[i].SetID(i);
    }

    public void RemoveAll() => allPaths.Clear();

    public void SetCurrentEdit(int _index)
    {
        currentPathIndex = _index;
    }
}
