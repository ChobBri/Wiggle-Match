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

        public void InitFill(GameObject[] gamePieces, SkinPack skin)
        {
            for (int j = 0; j < pieceQueue.Length; j++)
            {
                PieceSet pSet = CreatePieceSet(gamePieces, skin, j);
                pieceQueue[j] = pSet;
            }
        }

        public bool HasNext()
        {
            return pieceQueue[0] != null;
        }

        public PieceSet RetrieveNextPieceSet(GameObject[] gamePieces, SkinPack skin)
        {
            PieceSet nextPieceSet = pieceQueue[0];
            for (int i = 1; i < pieceQueue.Length; i++)
            {
                pieceQueue[i - 1] = pieceQueue[i];
            }
            pieceQueue[pieceQueue.Length - 1] = CreatePieceSet(gamePieces, skin, pieceQueue.Length - 1);
            return nextPieceSet;
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

        private PieceSet CreatePieceSet(GameObject[] gamePieces, SkinPack skin, int queuePosition = 0)
        {
            Piece[] pieceSetPieces = new Piece[3];

            for (int i = 0; i < pieceSetPieces.Length; i++)
            {
                pieceSetPieces[i] = Instantiate(gamePieces[Random.Range(0, gamePieces.Length)], gameObject.transform).GetComponent<Piece>();
                pieceSetPieces[i].GetComponent<SpriteRenderer>().sortingOrder = pieceSetPieces.Length - i;
                pieceSetPieces[i].transform.position = CellToWorld(new Vector2Int(queuePosition, i));
                ApplySkin(skin, pieceSetPieces[i]);
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
