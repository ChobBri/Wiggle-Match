using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject optionsScreen;
    private void Awake()
    {
        ScoreRecord.LoadHighScores();
        menuScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void StartGame()
    {
        ScoreRecord.totalSeconds = 0;
        SceneManager.LoadScene("Level 1");
    }

    public void OpenOptionsMenu()
    {
        menuScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void ExitOptionsMenu()
    {
        menuScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void EnterScoresMenu()
    {
        SceneManager.LoadScene("Score Screen");
    }

    public void ResetScores()
    {
        ScoreRecord.ResetHighScores();
    }
}
