using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public enum PieceColor { Red, Green, Blue, Yellow, Other }
    public class Piece : MonoBehaviour
    {
        [SerializeField] PieceColor color;
        public PieceColor Color { get => color; }
        public Vector2Int BoardPosition { get; set; }

    }
}
