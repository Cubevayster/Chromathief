using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ID.ToolBox.CustomHandles
{
    public static class HandlesTool
    {
        public static void DoPositionHandle(SerializedProperty _posProperty) => _posProperty.vector3Value = Handles.DoPositionHandle(_posProperty.vector3Value, Quaternion.identity);
        public static void DoPositionHandle(SerializedProperty _posProperty, Quaternion _rotation) => _posProperty.vector3Value = Handles.DoPositionHandle(_posProperty.vector3Value, _rotation);

        public static void DoPositionHandle(ref Vector3 _pos) => _pos = Handles.DoPositionHandle(_pos, Quaternion.identity);

        public static void DoPositionHandle(ref Vector3 _pos, Quaternion _rotation) => _pos = Handles.DoPositionHandle(_pos, Quaternion.identity);
        public static void DrawLine(Vector3 _begin, Vector3 _end, Color _color)
        {
            Handles.color = _color;
            Handles.DrawLine(_begin, _end);
            Handles.color = Color.white;
        }

        public static void DrawLine(SerializedProperty _begin, SerializedProperty _end, Color _color)
        {
            Handles.color = _color;
            Handles.DrawLine(_begin.vector3Value, _end.vector3Value);
            Handles.color = Color.white;
        }

        public static void DrawLines(Vector3[] _points, Color _color)
        {
            Handles.color = _color;
            Handles.DrawLines(_points);
            Handles.color = Color.white;
        }

        public static void DrawDottedLine(Vector3 _begin, Vector3 _end, Color _color, float _lineSpace = 1)
        {
            Handles.color = _color;
            Handles.DrawDottedLine(_begin, _end, _lineSpace);
            Handles.color = Color.white;
        }

        public static void DrawDottedLine(SerializedProperty _begin, SerializedProperty _end, Color _color, float _lineSpace = 1)
        {
            Handles.color = _color;
            Handles.DrawDottedLine(_begin.vector3Value, _end.vector3Value, _lineSpace);
            Handles.color = Color.white;
        }

        public static void DrawDottedLines(Vector3[] _points, Color _color, float _lineSpace = 1)
        {
            Handles.color = _color;
            Handles.DrawDottedLines(_points, _lineSpace);
            Handles.color = Color.white;
        }

        public static void DrawSphere(SerializedProperty _point, Color _color, float _size = 1, int _controlID = 0)
        {
            Handles.color = _color;
            Handles.SphereHandleCap(_controlID, _point.vector3Value, Quaternion.identity, _size, EventType.Repaint);
            Handles.color = Color.white;
        }

        public static void DrawSphere(Vector3 _point, Color _color, float _size = 1, int _controlID = 0)
        {
            Handles.color = _color;
            Handles.SphereHandleCap(_controlID, _point, Quaternion.identity, _size, EventType.Repaint);
            Handles.color = Color.white;
        }
    }
}
