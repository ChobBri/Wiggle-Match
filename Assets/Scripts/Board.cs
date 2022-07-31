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

        public void AssignPiece(Piece piece)
        {
            cells[piece.BoardPosition.x, piece.BoardPosition.y] = piece;
        }

        public void UnassignPiece(Vector2Int boardPosition)
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
