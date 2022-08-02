using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZL.Core;

namespace PZL.Movement
{
    public class PieceSetMover : MonoBehaviour
    {
        [SerializeField] Board board;

        public event System.Action<Piece[]> OnPieceCollision;
        public bool HasPieceSet { get => pieceSet != null; }
        PieceSet pieceSet;
        public void Move(Vector2Int direction)
        {
            Debug.Assert(Mathf.Abs(direction.x) + Mathf.Abs(direction.y) == 1);

            Piece[] pieces = pieceSet.Pieces;
            Vector2Int futurePos = pieces[0].BoardPosition + direction;
            if (futurePos.x < 0 || futurePos.x >= board.Width || futurePos.y >= board.Height || !board.IsEmpty(futurePos))
            {
                board.AssignPieces(pieces);
                pieceSet = null;
                OnPieceCollision?.Invoke(pieces);
                return;
            }

            MoveExceptHead(pieces);
            pieces[0].BoardPosition += direction;
            pieces[0].transform.position = board.CellToWorld(pieces[0].BoardPosition);
        }

        private void MoveExceptHead(Piece[] pieces)
        {
            for (int i = pieces.Length - 1; i > 0; i--)
            {
                pieces[i].BoardPosition = pieces[i - 1].BoardPosition;
                pieces[i].transform.position = board.CellToWorld(pieces[i].BoardPosition);
            }
        }

        public void AttachPieceSet(PieceSet pieceSet)
        {
            this.pieceSet = pieceSet;
        }
    }
}
