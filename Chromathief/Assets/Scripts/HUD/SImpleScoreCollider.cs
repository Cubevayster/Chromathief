using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SImpleScoreCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScoreManager score = GameObject.FindAnyObjectByType<ScoreManager>();
        score.currentScore += 1000;
        score.UpdateScore();
        Destroy(this.gameObject);
    }
}
