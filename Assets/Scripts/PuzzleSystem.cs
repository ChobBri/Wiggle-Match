using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZL.Movement;
using UnityEngine.SceneManagement;
using System;

namespace PZL.Core
{
    public class PuzzleSystem : MonoBehaviour
    {
        enum PuzzleState { Play, Complete, Pause, GameOver };
        [SerializeField] float entryDelay = 1.0f;
        float entryDelayTime = 0.0f;

        [SerializeField] CustomPacks customPacks;
        [SerializeField] GameObject[] gamePieces;
        [SerializeField] PieceSetMover mover;
        [SerializeField] Board board;
        [SerializeField] PieceQueue pieceQueue;
        [SerializeField] LevelNumber levelNumber;
        [SerializeField] LevelTimer levelTimer;
        [SerializeField] MusicPlayer musicPlayer;
        [SerializeField] GameOverSystem gameOverSystem;
        [SerializeField] PauseSystem pauseSystem;
        [SerializeField] GameObject gameOverPiece;
        [SerializeField] SfxPlayer sfxPlayer;

        PuzzleState state = PuzzleState.Play;

        bool isClearing = false;

        public event System.Action OnPuzzleComplete;

        private void Awake()
        {
            for (int i = 0; i < gamePieces.Length; i++)
            {
                // Don't want to modify prefab
                gamePieces[i] = Instantiate(gamePieces[i], new Vector3(100, 100, -100), Quaternion.identity);
            }
        }

        private IEnumerator Start()
        {
            mover.OnPieceCollision += ProcessPieceClear;

            SkinPack skin = customPacks.CurrentSkin;

            ApplySkin(skin);

            int levelNum = (levelNumber.Number - 1) / 5;
            switch (levelNum)
            {
                case 0:
                    musicPlayer.SetBGMClip(customPacks.CurrentMusic.BGM1);
                    break;
                case 1:
                    musicPlayer.SetBGMClip(customPacks.CurrentMusic.BGM2);
                    break;
                case 2:
                    musicPlayer.SetBGMClip(customPacks.CurrentMusic.BGM3);
                    break;
            }
            

            yield return new WaitForSeconds(1.0f);

            levelTimer.IsTimerRunning = true;

            

            pieceQueue.InitFill(gamePieces, skin);
        }

        private void ApplySkin(SkinPack skin)
        {

            gamePieces[0].GetComponent<SpriteRenderer>().sprite = skin.RedBlockSkin;
            gamePieces[1].GetComponent<SpriteRenderer>().sprite = skin.GreenBlockSkin;
            gamePieces[2].GetComponent<SpriteRenderer>().sprite = skin.YellowBlockSkin;
            gamePieces[3].GetComponent<SpriteRenderer>().sprite = skin.BlueBlockSkin;

            board.ApplySkin(skin);
        }

        private void ApplySkin(SkinPack skin, Piece piece)
        {
            switch (piece.Color)
            {
                case PieceColor.Red:
                    piece.GetComponent<SpriteRenderer>().sprite = piece.IsStatic ? skin.RedStaticBlockSkin : skin.RedBlockSkin;
                    break;
                case PieceColor.Green:
                    piece.GetComponent<SpriteRenderer>().sprite = piece.IsStatic ? skin.GreenStaticBlockSkin : skin.GreenBlockSkin;
                    break;
                case PieceColor.Yellow:
                    piece.GetComponent<SpriteRenderer>().sprite = piece.IsStatic ? skin.YellowStaticBlockSkin : skin.YellowBlockSkin;
                    break;
                case PieceColor.Blue:
                    piece.GetComponent<SpriteRenderer>().sprite = piece.IsStatic ? skin.BlueStaticBlockSkin : skin.BlueBlockSkin;
                    break;
            }
        }

        private void OnDestroy()
        {
            mover.OnPieceCollision -= ProcessPieceClear;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                    EndLevel();
            }
            if (Input.GetKeyDown(KeyCode.Period))
            {
                if(levelNumber.Number != 15)
                {
                    int bInd = SceneManager.GetActiveScene().buildIndex;
                    bInd += 1;
                    SceneManager.LoadScene(bInd);
                }
                
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                if (levelNumber.Number != 1)
                {
                    int bInd = SceneManager.GetActiveScene().buildIndex;
                    bInd -= 1;
                    SceneManager.LoadScene(bInd);
                }
            }
#endif
            switch (state)
            {
                case PuzzleState.Play:
                    ProcessPlay();
                    break;
                case PuzzleState.Complete:
                    break;
                case PuzzleState.Pause:
                    ProcessPause();
                    break;
                case PuzzleState.GameOver:
                    ProcessGameOver();
                    break;
                default:
                    break;
            }
        }


      

        private void ProcessPlay()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
            {
                pauseSystem.Pause();
                state = PuzzleState.Pause;
                return;
            }

            if (mover.HasPieceSet)
            {

            }
            else
            {
                if (entryDelayTime >= entryDelay)
                {
                    if (!pieceQueue.HasNext()) return;

                    if (!board.IsEmpty(new Vector2Int(board.Width / 2, board.Height - 1))) 
                    { 
                        var pieceset = ConstructNextPieceSet();
                        for (int i = 1; i < pieceset.Pieces.Length; i++)
                        {
                            Destroy(pieceset.Pieces[i].gameObject);
                        }

                        pieceset.Pieces[0].GetComponent<SpriteRenderer>().sortingOrder = pieceset.Pieces.Length + 1;
                        Piece deadPiece = board.UnassignPiece(new Vector2Int(board.Width / 2, board.Height - 1));
                        Destroy(deadPiece.gameObject);
                        board.AssignPiece(pieceset.Pieces[0]);
                        Die();
                        return;
                    }

                    DeployPieceSet(ConstructNextPieceSet());
                    entryDelayTime = 0.0f;
                }
                else
                {
                    if (!isClearing) entryDelayTime += Time.deltaTime;
                }
            }
        }

        private void ProcessPause()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                pauseSystem.Unpause();
                state = PuzzleState.Play;
                return;
            }
        }

        private void ProcessGameOver()
        {

        }

        private void DeployPieceSet(PieceSet pieceSet)
        {
            mover.AttachPieceSet(pieceSet);
        }

        private PieceSet ConstructNextPieceSet()
        {
            PieceSet nextPieceSet = pieceQueue.RetrieveNextPieceSet(gamePieces, customPacks.CurrentSkin);
            for (int i = 0; i < nextPieceSet.Pieces.Length; i++)
            {
                nextPieceSet.Pieces[i].GetComponent<SpriteRenderer>().sortingOrder = nextPieceSet.Pieces.Length - i;
                nextPieceSet.Pieces[i].BoardPosition = new Vector2Int(board.Width / 2, board.Height - 1);
                nextPieceSet.Pieces[i].transform.position = board.CellToWorld(nextPieceSet.Pieces[i].BoardPosition);
                nextPieceSet.Pieces[i].transform.SetParent(board.transform.GetChild(0).transform);
            }
            return nextPieceSet;
        }

        private void ProcessPieceClear(Piece[] pieces)
        {
            StartCoroutine(ProcessPieceClearCoroutine(pieces));
        }

        IEnumerator ProcessPieceClearCoroutine(Piece[] pieces)
        {
            isClearing = true;
            while (true)
            {
                bool hasCleared = false;
                foreach (var piece in pieces)
                {
                    if (AdjacentColorSize(piece.BoardPosition, piece.Color) >= 4)
                    {
                        hasCleared = true;
                        PieceClear(piece.BoardPosition, piece.Color);
                        sfxPlayer.PlayBlockClearSfx();
                    }
                }
                if(hasCleared) yield return new WaitForSeconds(0.3f);

                Piece[] changedPieces = new Piece[0];
                yield return StartCoroutine(board.GravityDrop( arr => changedPieces = arr ));
                if (changedPieces.Length > 0) yield return new WaitForSeconds(0.3f);
                else break;

                pieces = changedPieces;
            }
            isClearing = false;

            if (!board.HasTargetPiece())
            {
                OnPuzzleComplete?.Invoke();
                EndLevel();
            }
        }

        private void EndLevel()
        {
            state = PuzzleState.Complete;
            musicPlayer.PlayLevelClearJingle(customPacks.CurrentMusic.LevelClear);
            ScoreRecord.totalSeconds += levelTimer.Seconds;
            StartCoroutine(TransitionToNextLevel(customPacks.CurrentMusic.LevelClearTime));
            levelTimer.IsTimerRunning = false;
        }

        private int AdjacentColorSize(Vector2Int boardPosition, PieceColor color, bool[,] memo = null)
        {
            if (memo == null) memo = new bool[board.Width, board.Height];

            if (memo[boardPosition.x, boardPosition.y] == true ||
                board.IsEmpty(boardPosition) || 
                board.GetColor(boardPosition) != color) 
                    return 0;
            memo[boardPosition.x, boardPosition.y] = true;

            int sum = 1;
            if (boardPosition.x > 0) sum += AdjacentColorSize(boardPosition + Vector2Int.left, color, memo);
            if (boardPosition.x < board.Width - 1) sum += AdjacentColorSize(boardPosition + Vector2Int.right, color, memo);
            if (boardPosition.y > 0) sum += AdjacentColorSize(boardPosition + Vector2Int.down, color, memo);
            if (boardPosition.y < board.Height - 1) sum += AdjacentColorSize(boardPosition + Vector2Int.up, color, memo);

            return sum;
        }

        private void PieceClear(Vector2Int boardPosition, PieceColor color, bool[,] memo = null)
        {
            if (memo == null) memo = new bool[board.Width, board.Height];

            if (memo[boardPosition.x, boardPosition.y] == true ||
                board.IsEmpty(boardPosition) ||
                board.GetColor(boardPosition) != color)
                return;
            memo[boardPosition.x, boardPosition.y] = true;

            board.DestroyPiece(boardPosition);

            if (boardPosition.x > 0) PieceClear(boardPosition + Vector2Int.left, color, memo);
            if (boardPosition.x < board.Width - 1) PieceClear(boardPosition + Vector2Int.right, color, memo);
            if (boardPosition.y > 0) PieceClear(boardPosition + Vector2Int.down, color, memo);
            if (boardPosition.y < board.Height - 1) PieceClear(boardPosition + Vector2Int.up, color, memo);
        }

        private IEnumerator TransitionToNextLevel(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            if (SceneManager.GetActiveScene().name == "Level 15")
            {
                StartCoroutine(PlayGameOverAnimation());
            }
            else
            {
                int buildIndex = SceneManager.GetActiveScene().buildIndex;
                buildIndex += 1;
                buildIndex %= SceneManager.sceneCountInBuildSettings;
                SceneManager.LoadScene(buildIndex);
            }
        }

        private void Die()
        {
            state = PuzzleState.GameOver;
            levelTimer.IsTimerRunning = false;
            musicPlayer.Pause();
            StartCoroutine(PlayGameOverAnimation());
            //int buildIndex = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(buildIndex);
        }

        private IEnumerator PlayGameOverAnimation()
        {
            yield return new WaitForSeconds(1.0f);
            yield return PlayBoardShutDownAnimation();
            yield return new WaitForSeconds(1.0f);
            ScoreData data = new ScoreData(new char[] { 'A', 'B', 'C'} , ScoreRecord.totalSeconds, levelNumber.Number);
            bool isHighScore = ScoreRecord.IsNewHighScore(data);
            if (isHighScore)
            {
                musicPlayer.PlayNewHighScoreMusic();
                yield return gameOverSystem.ProcessNameEntry(ScoreRecord.totalSeconds, levelNumber.Number + (state == PuzzleState.Complete ? 1 : 0));
            }
            gameOverSystem.EnableGameOverScreen();
            musicPlayer.PlayGameOverJingle();
            yield return new WaitForSeconds(5.5f);

            SceneManager.LoadScene("Score Screen");
        }

        private IEnumerator PlayBoardShutDownAnimation()
        {
            for(int row = board.Height - 1; row >= 0; row--)
            {
                bool hasTileInRow = false;
                for (int col = 0; col < board.Width; col++)
                {
                    Vector2Int boardPos = new Vector2Int(col, row);
                    if (board.IsEmpty(boardPos)) continue;
                    Destroy(board.UnassignPiece(boardPos).gameObject);
                    Piece piece = Instantiate(gameOverPiece, board.CellToWorld(boardPos), Quaternion.identity).GetComponent<Piece>();
                    piece.BoardPosition = boardPos;
                    board.AssignPiece(piece); 
                    hasTileInRow = true;
                }
                if (hasTileInRow)
                {
                    sfxPlayer.PlayBlockSolidfySfx();
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }
}

