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
        SceneManager.LoadScene("Adelo");
        Debug.Log("You have clicked the button 'easy level'!");
    }

    public void openHardLevel()
    {
        SceneManager.LoadScene("Level1");
        Debug.Log("You have clicked the button 'hard level'!");
    }

    public void quitGame()
    {
        // TODO
        Debug.Log("You have clicked the button 'quit'!");
    }
}