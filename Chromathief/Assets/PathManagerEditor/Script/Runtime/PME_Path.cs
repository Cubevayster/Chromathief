using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PME_Path
{
    [SerializeField, Header("Path name(optionel) :")] string pathName = "Path";
    [SerializeField] bool isVisible = true;
    [SerializeField] int id = 0;
    [SerializeField] Color pathColor = Color.white;
    [SerializeField] List<Vector3> points = new List<Vector3>();

    public bool IsVisible = true;

    public List<Vector3> GetPathPoints() { return points; }

    public void AddPoint()
    {
        Vector3 _point = points.Count == 0 ? Vector3.zero : points[points.Count - 1] + Vector3.forward; //v[n] + v(0,0,1)
        points.Add(_point);
    }

    public void RemovePointAt(int _index) => points.RemoveAt(_index);

    public void RemoveAllPoints() => points.Clear();

    public void SetID(int _id) => id = _id;
}
