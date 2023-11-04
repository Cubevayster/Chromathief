using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    static public Action<Camera> OnGameOver = null;
    static public Action OnGameRestart = null;

    static float timeGameOver = 3;

    static public IEnumerator GameOver(Camera _cameraWhoSpotted)
    {
        OnGameOver?.Invoke(_cameraWhoSpotted);
        yield return new WaitForSeconds(timeGameOver);
        OnGameRestart?.Invoke();
        ReloadCurrentLevel();
    }

    static void ReloadCurrentLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
