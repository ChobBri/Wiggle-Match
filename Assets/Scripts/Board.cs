using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public class Board : MonoBehaviour
    {
        public int Width { get; } = 7;
        public int Height { get; } = 14;

        private Piece[,] cells;
        Grid grid;

        [SerializeField] Transform piecesTransform;
        private void Awake()
        {
            grid = GetComponent<Grid>();
            cells = new Piece[Width, Height];

            int length = piecesTransform.childCount;
            for (int i = 0; i < length; i++)
            {
                Piece piece = piecesTransform.GetChild(i).GetComponent<Piece>();
                piece.BoardPosition = WorldToCell(piece.transform.position);
                Debug.Assert(cells[piece.BoardPosition.x, piece.BoardPosition.y] == null, "Duplicate Blocks!");
                AssignPiece(piece);
                piece.transform.position = CellToWorld(piece.BoardPosition);
            }
        }

        private void Start()
        {
            
        }

        public void ApplySkin(Skin skin)
        {
            int length = piecesTransform.childCount;
            for (int i = 0; i < length; i++)
            {
                Piece piece = piecesTransform.GetChild(i).GetComponent<Piece>();
                switch (piece.Color)
                {
                    case PieceColor.Red:
                        piece.GetComponent<SpriteRenderer>().sprite = piece.IsStatic ? skin.redStaticBlockSkin : skin.redBlockSkin;
                        break;
                    case PieceColor.Green:
                        piece.GetComponent<SpriteRenderer>().sprite = piece.IsStatic ? skin.greenStaticBlockSkin : skin.greenBlockSkin;
                        break;
                    case PieceColor.Yellow:
                        piece.GetComponent<SpriteRenderer>().sprite = piece.IsStatic ? skin.yellowStaticBlockSkin : skin.yellowBlockSkin;
                        break;
                    case PieceColor.Blue:
                        piece.GetComponent<SpriteRenderer>().sprite = piece.IsStatic ? skin.blueStaticBlockSkin : skin.blueBlockSkin;
                        break;
                }
                
            }
        }

        public Vector2 CellToWorld(Vector2Int cellPosition)
        {
            return grid.CellToWorld((Vector3Int) cellPosition) + grid.cellSize/2.0f;
        }

        public Vector2Int WorldToCell(Vector2 worldPosition)
        {
            return (Vector2Int) grid.WorldToCell(worldPosition);
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
        public IEnumerator GravityDrop(System.Action<Piece[]> pieceSetter)
        {
            List<Piece> changedPieces = new();
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    Piece currentPiece = cells[col, row];
                    if (currentPiece == null) continue;
                    if (currentPiece.IsStatic) continue;

                    bool hasChangedState = false;

                    for (int height = currentPiece.BoardPosition.y - 1; height >= 0; height--)
                    {
                        Vector2Int nextPosition = new Vector2Int(currentPiece.BoardPosition.x, height);
                        if (IsEmpty(nextPosition))
                        {
                            hasChangedState = true;
                            UnassignPiece(currentPiece.BoardPosition);
                            currentPiece.BoardPosition = nextPosition;
                            AssignPiece(currentPiece);
                        } else
                        {
                            break;
                        }
                    }

                    if (hasChangedState) changedPieces.Add(currentPiece);
                    //currentPiece.transform.position = CellToWorld(currentPiece.BoardPosition);
                }
            }

            Piece[] returnPieces = changedPieces.ToArray();
            yield return SimulateGravityDrop(returnPieces);
            pieceSetter(returnPieces);

            yield return null;
        }

        private IEnumerator SimulateGravityDrop(Piece[] changedPieces)
        {
            Vector2 GRAVITY = new Vector2(0.0f, -40.0f);
            bool isDropping = true;
            while (isDropping)
            {
                isDropping = false;
                foreach(var piece in changedPieces)
                {
                    piece.velocity += GRAVITY * Time.deltaTime;
                    piece.transform.position += (Vector3) piece.velocity * Time.deltaTime;
                    if(piece.transform.position.y <= CellToWorld(piece.BoardPosition).y)
                    {
                        piece.transform.position = CellToWorld(piece.BoardPosition);
                    } else
                    {
                        isDropping = true;
                    }
                }
                yield return null;
            }

            foreach(var piece in changedPieces)
            {
                piece.velocity = Vector2.zero;
            }
            yield return null;
        }

        public bool HasTargetPiece()
        {
            for(int row = 0; row < Height; row++)
            {
                for(int col = 0; col < Width; col++)
                {
                    Piece piece = cells[col, row];
                    if (piece == null) continue;
                    if (piece.IsStatic && piece.Color != PieceColor.Other) return true;
                }
            }
            return false;
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
            cells[boardPosition.x, boardPosition.y].Die();
            cells[boardPosition.x, boardPosition.y] = null;
        }

        public void AssignPieces(Piece[] pieces)
        {
            foreach(var piece in pieces)
            {
                if(IsEmpty(piece.BoardPosition)) AssignPiece(piece);
                else
                {
                    Destroy(piece.gameObject);
                }
            }
        }

        public bool IsEmpty(Vector2Int boardPosition)
        {
            return cells[boardPosition.x, boardPosition.y] == null;
        }
    }
}
