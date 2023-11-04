using UnityEngine;
using UnityEditor;

public class PME_PathManagerEditorUtils
{
    [MenuItem("O3D/PathManager/Create")]
    static void CreateTool()
    {
        PME_PathManager _pathManager = GameObject.FindObjectOfType<PME_PathManager>();
        if (_pathManager) return;
        GameObject _pathManagerObject = new GameObject("Path manager",typeof(PME_PathManager));
        Selection.activeGameObject = _pathManagerObject;
    }

    [MenuItem("O3D/PathManager/Locate")]
    static void LocateTool()
    {
        PME_PathManager _pathManager = GameObject.FindObjectOfType<PME_PathManager>();
        if (!_pathManager)
        {
            bool _setup = EditorUtility.DisplayDialog("Tool setup", "Do you want to add a new Pathmanager in your scene ?", "Yes", "No");
            if (_setup) CreateTool();
            return;
        }
        Selection.activeGameObject = _pathManager.gameObject;
    }
}
