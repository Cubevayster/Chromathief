using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ButtonListeners : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button btn_easyLevel, btn_hardLevel, btn_quit;

    void Start()
    {

    }

    public void openEasyLevel()
    {
        SceneManager.LoadScene("Tutoriel");
        Debug.Log("You have clicked the button 'easy level'!");
    }

    public void openHardLevel()
    {
        SceneManager.LoadScene("Intro");
        Debug.Log("You have clicked the button 'hard level'!");
    }

    public void quitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("You have clicked the button 'quit'!");
    }
}
