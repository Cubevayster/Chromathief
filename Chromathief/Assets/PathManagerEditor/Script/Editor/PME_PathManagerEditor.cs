using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro.EditorUtilities;
using ID.ToolBox.Button;
using ID.ToolBox.Layout;
using ID.ToolBox.Style;

//Editor qui override un Runtime
[CustomEditor(typeof(PME_PathManager))]
public class PME_PathManagerEditor : Editor
{
    bool showPointOrder = false;
    bool showPointInfo = false;
    PME_PathManager pathManager = null;
    SerializedProperty allPathsProperty = null;

    #region Unity Methods
    private void OnEnable()
    {
        pathManager = (PME_PathManager)target;
        pathManager.name = "[MANAGERS]";
        Tools.current = Tool.None;
        InitEditor();
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawCheckboxs();
        DrawAllPathUI();
        AddPathUI();
        ClearPathUI();
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        serializedObject.Update();
        DrawPathPointsHandles(SelectPathToEdit(pathManager.CurrentPathIndex));
        serializedObject.ApplyModifiedProperties();
    }
    #endregion

    #region CustomMethods
    void InitEditor()
    {
        allPathsProperty = serializedObject.FindProperty("allPaths");
    }

    void DrawCheckboxs()
    {
        showPointOrder = EditorGUILayout.Toggle("Show Order", showPointOrder);
        showPointInfo = EditorGUILayout.Toggle("Show Info", showPointInfo);
    }

    void AddPathUI()
    {
        //GUI hérite de IMGUI
        EditorGUILayout.HelpBox("Add a new path", MessageType.Info);
        GUIButtonTools.CreateButton("Add path", Color.white, new Color(.16f, .4f, .2f), pathManager.AddPath);
        GUILayoutTools.Space(2);
    }

    void DrawPathUI(SerializedProperty _path, int _index)
    {
        SerializedProperty _colorProperty = null;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox($"Path {_index + 1}", MessageType.None);
        _colorProperty = _path.FindPropertyRelative("pathColor");
        _colorProperty.colorValue = EditorGUILayout.ColorField(_colorProperty.colorValue);
        GUIButtonTools.CreateButton("Edit", Color.white, pathManager.IsEditMatch(_index) ? Color.black : Color.grey, () => pathManager.SetCurrentEdit(_index));
        GUILayoutTools.Space(2);
        GUIButtonTools.CreateButton("+", Color.white, new Color(.16f, .4f, .2f), () => pathManager.AddPointAt(_index));
        GUIButtonTools.CreateButtonConfirmation("X", $"Suppress the path number {_index + 1} ?", "Yes", "No", Color.white, new Color(.6f, .1f, .1f), () => ClearPathCallback(_index));
        EditorGUILayout.EndHorizontal();
        GUILayoutTools.Space(1);
        DrawPathPointsUI(_path, _index);
    }

    void DrawAllPathUI()
    {
        EditorGUILayout.HelpBox("List of all path", MessageType.Info);
        for (int i = 0; i < allPathsProperty?.arraySize; i++)
        {
            PME_Path _p = pathManager.GetPath(i);
            GUILayoutTools.Fold(ref _p.IsVisible, $"Show/Hide path {i + 1}", true);
            //_p.IsVisible = EditorGUILayout.Foldout(_p.IsVisible, $"Show/Hide path {i + 1}", true);
            if (_p.IsVisible)
            {
                SerializedProperty _path = allPathsProperty.GetArrayElementAtIndex(i);
                DrawPathUI(_path, i);
            }
        }
        GUILayoutTools.Space(2);
    }

    void DrawPathPointsUI(SerializedProperty _path, int _pathIndex)
    {
        SerializedProperty _points = _path.FindPropertyRelative("points");
        for (int i = 0; i < _points.arraySize; i++)
        {
            Vector3 _point = _points.GetArrayElementAtIndex(i).vector3Value;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Vector3Field($"Path point {i + 1}", _point);
            GUILayoutTools.Space();
            GUIButtonTools.CreateButtonConfirmation("x",$"Suppress the point ?", "Yes", "No", Color.white, new Color(.6f, .1f, .1f), () => pathManager.RemovePathPointAt(_pathIndex, i));
            EditorGUILayout.EndHorizontal();
            GUILayoutTools.Space(1);
        }
        GUILayoutTools.Space();
        GUIButtonTools.CreateButtonConfirmation("Clear All points", $"Suppress all points  ?", "Yes", "No", Color.white, new Color(.6f, .1f, .1f), () => pathManager.RemoveAllPathPoints(_pathIndex));
        GUILayoutTools.Space(2);
    }

    void ClearPathUI()
    {
        if (pathManager.EmptyPath) return;
        EditorGUILayout.HelpBox("Remove all path", MessageType.Error);
        GUIButtonTools.CreateButtonConfirmation("Remove all path", "Suppress all paths?", "Yes", "No", Color.white, new Color(.6f, .1f, .1f), RemoveAllPathCallback);
        GUILayoutTools.Space(2);
    }

    void ClearPathCallback(int _i)
    {
        pathManager.RemoveAt(_i);
    }

    void RemoveAllPathCallback()
    {
        pathManager.RemoveAll();
    }

    SerializedProperty SelectPathToEdit(int _index)
    {
        if (_index < 0) return null;
        return allPathsProperty.GetArrayElementAtIndex(_index);
    }

    void DrawPathPointsHandles(SerializedProperty _path)
    {
        if (_path == null) return;
        SerializedProperty _color = _path.FindPropertyRelative("pathColor");
        SerializedProperty _points = _path.FindPropertyRelative("points");
        for (int i = 0; i < _points.arraySize; i++)
        {
            Handles.color = _color.colorValue;
            Vector3 _point = _points.GetArrayElementAtIndex(i).vector3Value;
            DrawPointInfo(_color.colorValue,_point, i);
            _point = Handles.DoPositionHandle(_point, Quaternion.identity);
            _points.GetArrayElementAtIndex(i).vector3Value = _point;
            if (i < _points.arraySize - 1)
            {
                Vector3 _to = _points.GetArrayElementAtIndex(i + 1).vector3Value;
                Handles.DrawDottedLine(_point, _to, 5f);
            }
            Handles.color = Color.white;
        }
    }

    void DrawPointInfo(Color _color,Vector3 _point,int _index)
    {
        Handles.color = _color - new Color(0, 0, 0, .8f);
        Handles.DrawSolidDisc(_point,Vector3.up, .5f);
        Handles.color = _color;
        Handles.Label(_point + Vector3.up * 4.5f, (showPointOrder ? $"Point {_index + 1}" : "") + (showPointInfo ? $"{_point}" : ""), GUIStyleTools.SetLabelStyle(_color, TextAnchor.MiddleCenter, FontStyle.Bold, 20));
    }

    #endregion

    #region Test

    #endregion
}
