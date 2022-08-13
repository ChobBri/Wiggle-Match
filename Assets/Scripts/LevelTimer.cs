using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] TMP_Text levelTimerText;

    public bool IsTimerRunning { get; set; } = false;

    public int Seconds { get => (int)totalTime; }
    float totalTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (IsTimerRunning)
        {
            totalTime += Time.deltaTime;

            int totalSeconds = (int)totalTime;
            int min = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            levelTimerText.text = $"{min}:{seconds:D2}";
        }
    }
}
