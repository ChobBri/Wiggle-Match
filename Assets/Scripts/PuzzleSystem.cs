using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZL.Movement;
using UnityEngine.SceneManagement;

namespace PZL.Core
{
    public class PuzzleSystem : MonoBehaviour
    {
        [SerializeField] float entryDelay = 1.0f;
        float entryDelayTime = 0.0f;

        [SerializeField] GameObject[] gamePieces;
        [SerializeField] PieceSetMover mover;
        [SerializeField] Board board;

        bool isClearing = false;



        private void Start()
        {
            mover.OnPieceCollision += ProcessPieceClear;
        }

        private void OnDestroy()
        {
            mover.OnPieceCollision -= ProcessPieceClear;
        }

        private void Update()
        {
            if (mover.HasPieceSet)
            {

            } else
            {
                if(entryDelayTime >= entryDelay)
                {
                    CreateNewPieceSet();
                    entryDelayTime = 0.0f;
                } else
                {
                    if(!isClearing) entryDelayTime += Time.deltaTime;
                }
            }
        }

        private void CreateNewPieceSet()
        {
            Piece[] pieceSetPieces = new Piece[3];
            for (int i = 0; i < pieceSetPieces.Length; i++)
            {
                pieceSetPieces[i] = Instantiate(gamePieces[Random.Range(0, gamePieces.Length)], board.gameObject.transform).GetComponent<Piece>();
                pieceSetPieces[i].GetComponent<SpriteRenderer>().sortingOrder = pieceSetPieces.Length - i;
                pieceSetPieces[i].BoardPosition = new Vector2Int(Board.Width / 2, 0);
                pieceSetPieces[i].transform.position = board.CellToWorld(pieceSetPieces[i].BoardPosition);

            }
            PieceSet pSet = new PieceSet(pieceSetPieces);
            mover.AttachPieceSet(pSet);
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
                foreach (var piece in pieces)
                {

                    if (AdjacentColorSize(piece.BoardPosition, piece.Color) >= 4)
                    {
                        PieceClear(piece.BoardPosition, piece.Color);
                    }
                }
                yield return new WaitForSeconds(0.5f);

                Piece[] changedPieces = board.GravityDrop();
                if (changedPieces.Length > 0) yield return new WaitForSeconds(0.5f);
                else break;

                pieces = changedPieces;
            }
            isClearing = false;

            if (!board.HasStaticPiece())
            {
                MoveToNextLevel();
            }
        }

        private int AdjacentColorSize(Vector2Int boardPosition, PieceColor color, bool[,] memo = null)
        {
            if (memo == null) memo = new bool[Board.Width, Board.Height];

            if (memo[boardPosition.x, boardPosition.y] == true ||
                board.IsEmpty(boardPosition) || 
                board.GetColor(boardPosition) != color) 
                    return 0;
            memo[boardPosition.x, boardPosition.y] = true;

            int sum = 1;
            if (boardPosition.x > 0) sum += AdjacentColorSize(boardPosition + Vector2Int.left, color, memo);
            if (boardPosition.x < Board.Width - 1) sum += AdjacentColorSize(boardPosition + Vector2Int.right, color, memo);
            if (boardPosition.y > 0) sum += AdjacentColorSize(boardPosition + Vector2Int.down, color, memo);
            if (boardPosition.y < Board.Height - 1) sum += AdjacentColorSize(boardPosition + Vector2Int.up, color, memo);

            return sum;
        }

        private void PieceClear(Vector2Int boardPosition, PieceColor color, bool[,] memo = null)
        {
            if (memo == null) memo = new bool[Board.Width, Board.Height];

            if (memo[boardPosition.x, boardPosition.y] == true ||
                board.IsEmpty(boardPosition) ||
                board.GetColor(boardPosition) != color)
                return;
            memo[boardPosition.x, boardPosition.y] = true;

            board.DestroyPiece(boardPosition);

            if (boardPosition.x > 0) PieceClear(boardPosition + Vector2Int.left, color, memo);
            if (boardPosition.x < Board.Width - 1) PieceClear(boardPosition + Vector2Int.right, color, memo);
            if (boardPosition.y > 0) PieceClear(boardPosition + Vector2Int.down, color, memo);
            if (boardPosition.y < Board.Height - 1) PieceClear(boardPosition + Vector2Int.up, color, memo);
        }

        private void MoveToNextLevel()
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            buildIndex += 1;
            buildIndex %= SceneManager.sceneCount;
            SceneManager.LoadScene(buildIndex);
        }
    }
}
