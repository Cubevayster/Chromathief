using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    static public bool gameIsOver = false;
    static public Action<Camera> OnGameOver = null;
    static public Action OnGameRestart = null;

    static float timeGameOver = 3;

    static public IEnumerator GameOver(Camera _cameraWhoSpotted)
    {
        gameIsOver = true;
        OnGameOver?.Invoke(_cameraWhoSpotted);
        yield return new WaitForSeconds(timeGameOver);
        OnGameRestart?.Invoke();
        ReloadCurrentLevel();
        gameIsOver = false;
    }

    static void ReloadCurrentLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
