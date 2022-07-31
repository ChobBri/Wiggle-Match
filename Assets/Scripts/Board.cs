using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public class Board : MonoBehaviour
    {
        public const int Width = 7;
        public const int Height = 12;

        private Piece[,] cells;
        public Grid grid;

        private void Awake()
        {
            grid = GetComponent<Grid>();
            cells = new Piece[Width, Height];
        }

        public Vector2 CellToWorld(Vector2Int cellPosition)
        {
            return grid.CellToWorld((Vector3Int) cellPosition) + grid.cellSize/2.0f;
        }

        public PieceColor GetColor(Vector2Int boardPosition)
        {
            Debug.Assert(cells[boardPosition.x, boardPosition.y] != null);
            return cells[boardPosition.x, boardPosition.y].Color;
        }

        /// <summary>
        /// Applies gravity on all pieces.
        /// Returns whether the board state was changed.
        /// </summary>
        public Piece[] GravityDrop()
        {
            List<Piece> changedPieces = new();
            for (int row = Board.Height - 1; row >= 0; row--)
            {
                for (int col = 0; col < Board.Width; col++)
                {
                    Piece currentPiece = cells[col, row];
                    if (currentPiece == null) continue;

                    bool hasChangedState = false;

                    for (int height = currentPiece.BoardPosition.y + 1; height < Board.Height; height++)
                    {
                        Vector2Int nextPosition = new Vector2Int(currentPiece.BoardPosition.x, height);
                        if (IsEmpty(nextPosition))
                        {
                            hasChangedState = true;
                            UnassignPiece(currentPiece.BoardPosition);
                            currentPiece.BoardPosition = nextPosition;
                            AssignPiece(currentPiece);
                        }
                    }

                    if (hasChangedState) changedPieces.Add(currentPiece);
                    currentPiece.transform.position = CellToWorld(currentPiece.BoardPosition);
                }
            }
            return changedPieces.ToArray();
        }

        public void AssignPiece(Piece piece)
        {
            cells[piece.BoardPosition.x, piece.BoardPosition.y] = piece;
        }

        public Piece UnassignPiece(Vector2Int boardPosition)
        {
            Piece returnPiece = cells[boardPosition.x, boardPosition.y];
            cells[boardPosition.x, boardPosition.y] = null;
            return returnPiece;
        }

        public void DestroyPiece(Vector2Int boardPosition)
        {
            Destroy(cells[boardPosition.x, boardPosition.y].gameObject);
            cells[boardPosition.x, boardPosition.y] = null;
        }

        public void AssignPieces(Piece[] pieces)
        {
            foreach(var piece in pieces)
            {
                AssignPiece(piece);
            }
        }

        public bool IsEmpty(Vector2Int boardPosition)
        {
            return cells[boardPosition.x, boardPosition.y] == null;
        }
    }
}
