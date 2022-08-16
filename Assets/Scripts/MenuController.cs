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
        PlayerPrefs.Save();
    }

    public void EnterScoresMenu()
    {
        SceneManager.LoadScene("Score Screen");
    }

    public void ResetScores()
    {
        ScoreRecord.ResetHighScores();
    }

    public void QuitGame()
    {
            // save any game data here
    #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
    #else
             Application.Quit();
    #endif
    }
}
