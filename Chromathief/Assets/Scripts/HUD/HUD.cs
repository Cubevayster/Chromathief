using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

using GameColor = ColorManager.GameColor;

[Serializable]
public struct AlertColors
{
    [SerializeField] ALERT_TYPES mTypes;
    [SerializeField] Color mTriangleColor;
    [SerializeField] Color mExclamationColor;

    public ALERT_TYPES AlertType() => mTypes;
    public Color TriangleColor() => mTriangleColor;
    public Color ExclamationColor() => mExclamationColor;
}

public class HUD : Singleton<HUD>
{
    [SerializeField] TextMeshProUGUI mLevelTxt = null;
    [SerializeField] TextMeshProUGUI mScoreTimeTxt = null;

    [SerializeField] Image currentColor;
    [SerializeField] Image red;
    [SerializeField] bool redIsToggled = false;
    [SerializeField] Image blue = null;
    [SerializeField] bool blueIsToggled = false;
    [SerializeField] Image yellow = null;
    [SerializeField] bool yellowIsToggled = false;

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

    public void SetPrimaryColors(bool _red = false, bool _blue = false, bool _yellow = false)
    {
        redIsToggled = _red; UpdateUIPrimaryColors(red, redIsToggled);
        blueIsToggled = _blue; UpdateUIPrimaryColors(blue, blueIsToggled);
        yellowIsToggled = _yellow; UpdateUIPrimaryColors(yellow, yellowIsToggled);
    }

    public void TogglePrimaryColor(GameColor _colorToToggle, bool _isToggled)
    {
        switch (_colorToToggle)
        {
            case GameColor.Red:
                redIsToggled = _isToggled;
                UpdateUIPrimaryColors(red, redIsToggled);
                break;
            case GameColor.Blue:
                blueIsToggled = _isToggled;
                UpdateUIPrimaryColors(blue, blueIsToggled);
                break;
            case GameColor.Yellow:
                yellowIsToggled = _isToggled;
                UpdateUIPrimaryColors(yellow, yellowIsToggled);
                break;
            default:
                break;
        }
    }

    public void UpdateUIPrimaryColors(Image _primaryColor, bool _toggled)
    {
        if (!_primaryColor) return;
        _primaryColor.enabled = _toggled;
    }

    public void SetCurrentColor(GameColor _color)
    {
        if (!currentColor) return;
        currentColor.color = ColorManager.GetColor(_color);
    }

    public void SetAlert(ALERT_TYPES _alert)
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
        UpdateUIPrimaryColors(red, redIsToggled);
        UpdateUIPrimaryColors(blue, blueIsToggled);
        UpdateUIPrimaryColors(yellow, yellowIsToggled);
    }
}
