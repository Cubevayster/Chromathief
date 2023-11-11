using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI scoretext;
    public int currentScore = 0;

    private void Start()
    {
        scoretext.text = "Score : 0$";
    }

    public void UpdateScore()
    {
        scoretext.text = "Score : "+currentScore+"$";
    }
}
