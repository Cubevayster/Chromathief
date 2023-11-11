using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        PlayerControler controller = collider.GetComponent<PlayerControler>();

        if(controller != null)
        {
            if (controller.FoundExitKey)
            {
                SceneManager.LoadScene("VictoryScene");
            }
        }
    }
}
