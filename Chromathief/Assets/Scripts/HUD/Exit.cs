using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI temp;
    private void OnTriggerEnter(Collider collider)
    {
        PlayerControler controller = collider.GetComponent<PlayerControler>();
      


        if(controller != null)
        {
            if (controller.FoundExitKey)
            {
                CustomManager.Instance.ScoreText = score.text;
                CustomManager.Instance.TimeText = temp.text;
                SceneManager.LoadScene("VictoryScene");
            }
        }
    }
}
