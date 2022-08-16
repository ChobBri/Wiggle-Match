using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverSystem : MonoBehaviour
{

    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] GameObject letterSlots;
    [SerializeField] GameObject nameEntrySystem;
    [SerializeField] TMP_Text rankText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text levelText;



    int nameIndex = 0;

    private void Awake()
    {
        gameOverCanvas.enabled = false;
    }

    public void EnableGameOverScreen()
    {
        gameOverCanvas.enabled = true;
        gameOverText.enabled = true;
        nameEntrySystem.SetActive(false);
    }

    public IEnumerator ProcessNameEntry(int totalSeconds, int highestLevel)
    {
        gameOverCanvas.enabled = true;
        gameOverText.enabled = false;
        nameEntrySystem.SetActive(true);


        letterSlots.transform.GetChild(3).GetComponent<TMP_Text>().enabled = false;

        int rank = ScoreRecord.GetRankNumber(new ScoreData(new char[] {'A', 'A', 'A' }, totalSeconds, highestLevel));
        rankText.text = $"Rank {rank}.";

        int min = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        timeText.text = $"Total Time:\n{min}:{seconds:D2}";
        levelText.text = $"Highest Stage:\n{highestLevel}";
        while(nameIndex < 4)
        {
            var text = letterSlots.transform.GetChild(nameIndex).GetComponent<TMP_Text>();
            text.fontStyle = FontStyles.Underline;
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (nameIndex <= 2)
                {
                    char c = text.text[0];
                    c--;
                    if (c == 'A' - 1) c = ' ';
                    else if (c == ' ' - 1) c = 'Z';
                    text.text = "" + c;
                } else
                {
                    nameIndex = 4;
                }
                
            } else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (nameIndex <= 2)
                {
                    char c = text.text[0];
                    c++;
                    if (c == 'Z' + 1) c = ' ';
                    else if (c == ' ' + 1) c = 'A';
                    text.text = "" + c;
                } else
                {
                    nameIndex = 4;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nameIndex++;
                if (nameIndex == 3 && !letterSlots.transform.GetChild(nameIndex).GetComponent<TMP_Text>().enabled)
                {
                    letterSlots.transform.GetChild(nameIndex).GetComponent<TMP_Text>().enabled = true;
                }
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (nameIndex == 3 && letterSlots.transform.GetChild(nameIndex).GetComponent<TMP_Text>().enabled)
                {
                    letterSlots.transform.GetChild(nameIndex).GetComponent<TMP_Text>().enabled = false;
                }
                nameIndex--;
                nameIndex = Mathf.Max(nameIndex, 0);
            }

            yield return null;
            text.fontStyle = FontStyles.Normal;
        }
        char[] initials = new char[] {
            letterSlots.transform.GetChild(0).GetComponent<TMP_Text>().text[0],
            letterSlots.transform.GetChild(1).GetComponent<TMP_Text>().text[0],
            letterSlots.transform.GetChild(2).GetComponent<TMP_Text>().text[0]
        };

        ScoreData newData = new ScoreData(initials, totalSeconds, highestLevel);
        ScoreRecord.UpdateHighScores(newData);
        yield return null;
    }
}
