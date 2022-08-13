using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] GameObject scores;
    private void Start()
    {
        for(int i = 0; i < scores.transform.childCount; i++)
        {
            Score score = scores.transform.GetChild(i).GetComponent<Score>();
            score.SetData(i + 1, ScoreRecord.highScores[i]);
        }
    }
}
