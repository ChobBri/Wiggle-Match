using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public class PieceQueue : MonoBehaviour
    {
        PieceSet[] pieceQueue;
        Grid grid;

        private void Awake()
        {
            pieceQueue = new PieceSet[1];
            grid = GetComponent<Grid>();
        }

        public void InitFill(GameObject[] gamePieces)
        {
            for (int j = 0; j < pieceQueue.Length; j++)
            {
                PieceSet pSet = CreatePieceSet(gamePieces, j);
                pieceQueue[j] = pSet;
            }
        }

        public PieceSet RetrieveNextPieceSet(GameObject[] gamePieces)
        {
            PieceSet nextPieceSet = pieceQueue[0];
            for (int i = 1; i < pieceQueue.Length; i++)
            {
                pieceQueue[i - 1] = pieceQueue[i];
            }
            pieceQueue[pieceQueue.Length - 1] = CreatePieceSet(gamePieces, pieceQueue.Length - 1);
            return nextPieceSet;
        }

        private PieceSet CreatePieceSet(GameObject[] gamePieces, int queuePosition = 0)
        {
            Piece[] pieceSetPieces = new Piece[3];

            for (int i = 0; i < pieceSetPieces.Length; i++)
            {
                pieceSetPieces[i] = Instantiate(gamePieces[Random.Range(0, gamePieces.Length)], gameObject.transform).GetComponent<Piece>();
                pieceSetPieces[i].GetComponent<SpriteRenderer>().sortingOrder = pieceSetPieces.Length - i;
                pieceSetPieces[i].transform.position = CellToWorld(new Vector2Int(queuePosition, i));
            }
            PieceSet pSet = new PieceSet(pieceSetPieces);
            return pSet;
        }

        private Vector2 CellToWorld(Vector2Int cellPosition)
        {
            return grid.CellToWorld((Vector3Int)cellPosition) + grid.cellSize / 2.0f;
        }
    }
}
