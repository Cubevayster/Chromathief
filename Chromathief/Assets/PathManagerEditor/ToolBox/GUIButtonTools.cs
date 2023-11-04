using System;
using ID.ToolBox.Style;
using UnityEditor;
using UnityEngine;

namespace ID.ToolBox.Button
{
    public class GUIButtonTools
    {
        public static void CreateButton(string _label, Color _colorText, Color _colorBackground, Action _callback, int _fontSize = 12, TextAnchor _anchor = TextAnchor.MiddleCenter, FontStyle _fontStyle = FontStyle.Bold)
        {
            if (GUILayout.Button(_label, GUIStyleTools.SetButtonStyle(_colorText, _colorBackground, _anchor, _fontStyle, _fontSize)))
            {
                _callback?.Invoke();
            }
        }

        /*public static void CreateButton<T>(string _label, Color _colorText, Color _colorBackground, Action<T> _callback, T _arg0, int _fontSize = 12, TextAnchor _anchor = TextAnchor.MiddleCenter, FontStyle _fontStyle = FontStyle.Bold)
        {
            if (GUILayout.Button(_label, GUIStyleTools.SetButtonStyle(_colorText, _colorBackground, _anchor, _fontStyle, _fontSize)))
            {
                _callback?.Invoke(_arg0);
            }
        }*/

        public static void CreateButton(Texture _texture, Color _colorText, Color _colorBackground, Action _callback, int _fontSize = 12, TextAnchor _anchor = TextAnchor.MiddleCenter, FontStyle _fontStyle = FontStyle.Bold)
        {
            if (GUILayout.Button(_texture, GUIStyleTools.SetButtonStyle(_colorText, _colorBackground, _anchor, _fontStyle, _fontSize)))
            {
                _callback?.Invoke();
            }
        }

        public static void CreateButtonConfirmation(string _label, string _confimationMsg, Color _colorText, Color _colorBackground, Action _callback, int _fontSize = 12, TextAnchor _anchor = TextAnchor.MiddleCenter, FontStyle _fontStyle = FontStyle.Bold)
        {
            if (GUILayout.Button(_label, GUIStyleTools.SetButtonStyle(_colorText, _colorBackground, _anchor, _fontStyle, _fontSize)))
            {

                bool _isReady = EditorUtility.DisplayDialog(_confimationMsg, _confimationMsg, "Yes", "No");
                if (_isReady)
                    _callback?.Invoke();
            }
        }

        public static void CreateButtonConfirmation(string _label, string _confimationMsg, string _okMsg, string _cancelMsg, Color _colorText, Color _colorBackground, Action _callback, int _fontSize = 12, TextAnchor _anchor = TextAnchor.MiddleCenter, FontStyle _fontStyle = FontStyle.Bold)
        {
            if (GUILayout.Button(_label, GUIStyleTools.SetButtonStyle(_colorText, _colorBackground, _anchor, _fontStyle, _fontSize)))
            {

                bool _isReady = EditorUtility.DisplayDialog(_confimationMsg, _confimationMsg, _okMsg, _cancelMsg);
                if (_isReady)
                    _callback?.Invoke();
            }
        }


        public static void CreateImgButton(Texture2D _img, Action _callback)
        {
            if (GUILayout.Button(_img))
                _callback?.Invoke();
        }
    }
}
