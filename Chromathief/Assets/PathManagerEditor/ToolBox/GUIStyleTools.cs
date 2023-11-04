using UnityEngine;

namespace ID.ToolBox.Style
{
    public class GUIStyleTools
    {
        #region Style
        public static GUIStyle SetButtonStyle(Color _textColor, Color _backgroundColor, TextAnchor _alignement, FontStyle _fontStyle, int _size = 12)
        {
            GUIStyle _style = new GUIStyle(GUI.skin.button);
            _style.normal.background = GetPixelColor(_backgroundColor);
            _style.normal.textColor = _textColor;
            _style.alignment = _alignement;
            _style.fontStyle = _fontStyle;
            _style.fontSize = _size;
            return _style;
        }

        public static GUIStyle SetLabelStyle(Color _textColor, TextAnchor _alignement, FontStyle _fontStyle, int _size = 12)
        {
            GUIStyle _style = new GUIStyle(GUI.skin.label);
            _style.normal.textColor = _textColor;
            _style.alignment = _alignement;
            _style.fontStyle = _fontStyle;
            _style.fontSize = _size;
            return _style;
        }

        public static GUIStyle SetLabelStyle(Color _textColor, Color _backColor, TextAnchor _alignement, FontStyle _fontStyle, int _size = 12)
        {
            GUIStyle _style = new GUIStyle(GUI.skin.label);
            _style.normal.textColor = _textColor;
            _style.normal.background = GetPixelColor(_backColor);
            _style.alignment = _alignement;
            _style.fontStyle = _fontStyle;
            _style.fontSize = _size;
            return _style;
        }

        public static Texture2D GetPixelColor(Color _color)
        {
            Texture2D _t = new Texture2D(1, 1);
            _t.SetPixel(1, 1, _color);
            _t.Apply();
            return _t;
        }
        #endregion
    }
}