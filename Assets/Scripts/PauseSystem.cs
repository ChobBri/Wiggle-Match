using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZL.Core;
using PZL.Controls;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] MusicPlayer musicPlayer;
    [SerializeField] PuzzleController puzzleController;
    [SerializeField] Board board;
    [SerializeField] PieceQueue pieceQueue;
    [SerializeField] SpriteRenderer levelInfoBorderSprite;
    [SerializeField] SpriteRenderer backgroundSprite;
    [SerializeField] Canvas pauseCanvas;

    private void Awake()
    {
        pauseCanvas.enabled = false;
    }


    public void Pause()
    {
        Time.timeScale = 0;

        musicPlayer.Pause();
        puzzleController.enabled = false;

        pauseCanvas.enabled = true;
        board.GetComponent<SpriteRenderer>().sortingLayerName = "Pause";
        pieceQueue.GetComponent<SpriteRenderer>().sortingLayerName = "Pause";
        levelInfoBorderSprite.sortingLayerName = "Pause";
        backgroundSprite.sortingLayerName = "Pause";
    }

    public void Unpause()
    {
        Time.timeScale = 1;

        musicPlayer.Play();
        puzzleController.enabled = true;

        pauseCanvas.enabled = false;
        board.GetComponent<SpriteRenderer>().sortingLayerName = "Board";
        pieceQueue.GetComponent<SpriteRenderer>().sortingLayerName = "Board";
        levelInfoBorderSprite.sortingLayerName = "Board";
        backgroundSprite.sortingLayerName = "Background";
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
