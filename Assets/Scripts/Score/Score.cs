using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text rankText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text levelText;

    public void SetData(int rank, ScoreData data)
    {
        rankText.text = $"{rank}.";
        nameText.text = new string(data.initials);

        int totalSeconds = data.totalSeconds;
        int min = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        timeText.text = $"{min}:{seconds:D2}";

        levelText.text = $"{data.highestLevel}";
    }
}
