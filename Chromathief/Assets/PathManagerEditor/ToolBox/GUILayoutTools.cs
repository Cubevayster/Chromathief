using UnityEditor;
using UnityEngine;

namespace ID.ToolBox.Layout
{
    public class GUILayoutTools
    {
        public static void Space(int _number = 1)
        {
            for (int i = 0; i < _number; i++)
                EditorGUILayout.Space();
        }

        public static void Fold(ref bool _value, string _label, bool _toggle = true)
        {
            _value = EditorGUILayout.Foldout(_value, _label, _toggle);
        }

        public static void ColorField(SerializedProperty _colorProperty) => _colorProperty.colorValue = EditorGUILayout.ColorField(_colorProperty.colorValue);

        public static void ColorField(string _label, SerializedProperty _colorProperty) => _colorProperty.colorValue = EditorGUILayout.ColorField(_label, _colorProperty.colorValue);

        public static void ColorField(ref Color _color) => _color = EditorGUILayout.ColorField(_color);

        public static void ColorField(string _label, ref Color _color) => _color = EditorGUILayout.ColorField(_label, _color);

        public static void IntSlider(ref int _value, int _min, int _max, string _label = "")
        {
            _value = EditorGUILayout.IntSlider(_label, _value, _min, _max);
        }

        public static void IntSlider(SerializedProperty _value, int _min, int _max, string _label = "")
        {
            _value.intValue = EditorGUILayout.IntSlider(_label, _value.intValue, _min, _max);
        }

        public static void ObjectField<T> (SerializedProperty _value, bool _allowSceneAsset = false, string _label = "") where T : Object
        {
            _value.objectReferenceValue = (T)EditorGUILayout.ObjectField(_label, _value.objectReferenceValue, typeof(T), _allowSceneAsset);
        }

        public static void ObjectField<T>(ref Object _value, bool _allowSceneAsset = false, string _label = "") where T : Object
        {
            _value = (T)EditorGUILayout.ObjectField(_label, _value, typeof(T), _allowSceneAsset);
        }
    }
}
