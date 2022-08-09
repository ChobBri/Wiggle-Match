using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZL.Core;
using PZL.Controls;

public class PauseSystem : MonoBehaviour
{
    bool paused = false;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
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
            else
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
        }
    }
}
