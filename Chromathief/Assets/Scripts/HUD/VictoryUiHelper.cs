using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryUiHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI temps;

    void Start()
    {
        temps.text = "temps : " + CustomManager.Instance.TimeText;
        score.text = CustomManager.Instance.ScoreText;
    }

}
