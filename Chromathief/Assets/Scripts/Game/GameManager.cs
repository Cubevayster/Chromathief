using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    static public bool gameIsOver = false;
    static public Action<SpotcamComponent> OnGameOver = null;
    static public Action OnGameRestart = null;

    static float timeGameOver = 3;

    static float timer = 0;

    static public string UpdateTimer(float deltaTime)
    {
        timer += deltaTime;
        string timerDisplay = "";
        int _nbMinutes = (int)timer / 60;
        int _nbSeconds = (int)timer % 60;
        float _decimal = (float)(timer - Math.Truncate(timer));
        while (_decimal < 100)
            _decimal *= 10;
        timerDisplay += (_nbMinutes < 10 ? "0" + _nbMinutes : _nbMinutes)
                        + ":" + (_nbSeconds < 10 ? "0" + _nbSeconds : _nbSeconds)
                        + ":" + ((int)_decimal < 10 ? "00" + (int)_decimal : (int)_decimal < 100 ? "0" + (int)_decimal : (int)_decimal);
        return timerDisplay;
    }

    static public IEnumerator GameOver(SpotcamComponent _spotCam)
    {
        if (gameIsOver) yield break;
        _spotCam.ActivateSpotCamera();
        gameIsOver = true;
        OnGameOver?.Invoke(_spotCam);
        yield return new WaitForSeconds(timeGameOver);
        OnGameRestart?.Invoke();
        ReloadCurrentLevel();
        gameIsOver = false;
    }

    static void ReloadCurrentLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
