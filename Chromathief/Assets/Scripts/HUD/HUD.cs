using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

using GameColor = ColorManager.GameColor;

[Serializable]
public struct AlertColors
{
    [SerializeField] AlertTypes mTypes;
    [SerializeField] Color mTriangleColor;
    [SerializeField] Color mExclamationColor;

    public AlertTypes AlertType() => mTypes;
    public Color TriangleColor() => mTriangleColor;
    public Color ExclamationColor() => mExclamationColor;
}

public class HUD : Singleton<HUD>
{
    [SerializeField] TextMeshProUGUI mLevelTxt = null;
    [SerializeField] TextMeshProUGUI mScoreTimeTxt = null;

    [SerializeField] RawImage mCurrentColor = null;
    [SerializeField] RawImage mRed = null;
    [SerializeField] bool mRedIsToggled = false;
    [SerializeField] RawImage mBlue = null;
    [SerializeField] bool mBlueIsToggled = false;
    [SerializeField] RawImage mYellow = null;
    [SerializeField] bool mYellowIsToggled = false;
    [SerializeField, Range(0,1)] float mOpacityToggledOff = 0.2f;
    [SerializeField, Range(0,1)] float mOpacityToggledOn = 1;

    [SerializeField] RawImage mTriangleWarning = null;
    [SerializeField] RawImage mExclamationWarning = null;
    [SerializeField] List<AlertColors> mColorsAlerts = new List<AlertColors>();

    public void SetLvlName(string _lvlName)
    {
        if (!mLevelTxt) return;
        mLevelTxt.text = _lvlName;
    }

    public void SetScoreTime(string _scoreTime)
    {
        if (!mScoreTimeTxt) return;
        mScoreTimeTxt.text = _scoreTime;
    }

    public void TogglePrimaryColor(GameColor _colorToToggle, bool _isToggled)
    {
        switch (_colorToToggle)
        {
            case GameColor.Red:
                mRedIsToggled = _isToggled;
                UpdateUIPrimaryColors(mRed, mRedIsToggled);
                break;
            case GameColor.Blue:
                mBlueIsToggled = _isToggled;
                UpdateUIPrimaryColors(mBlue, mBlueIsToggled);
                break;
            case GameColor.Yellow:
                mYellowIsToggled = _isToggled;
                UpdateUIPrimaryColors(mYellow, mYellowIsToggled);
                break;
            default:
                break;
        }
    }

    public void UpdateUIPrimaryColors(RawImage _primaryColor, bool _toggled)
    {
        if (!_primaryColor) return;
        Color _newColor = _primaryColor.color;
        _newColor.a = _toggled ? mOpacityToggledOn : mOpacityToggledOff;
        _primaryColor.color = _newColor;
    }

    public void SetCurrentColor(GameColor _color)
    {
        if (!mCurrentColor) return;
        mCurrentColor.color = ColorManager.GetColor(_color);
    }

    public void SetAlert(AlertTypes _alert)
    {
        if (!mTriangleWarning || !mExclamationWarning) return;
        foreach (AlertColors _color in mColorsAlerts)
        {
            if (_color.AlertType() != _alert) continue;
            mTriangleWarning.color = _color.TriangleColor();
            mExclamationWarning.color = _color.ExclamationColor();
            return;
        }
    }

    private void Start()
    {
        UpdateUIPrimaryColors(mRed, mRedIsToggled);
        UpdateUIPrimaryColors(mBlue, mBlueIsToggled);
        UpdateUIPrimaryColors(mYellow, mYellowIsToggled);
    }
}
