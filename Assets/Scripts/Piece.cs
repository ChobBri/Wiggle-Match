using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public enum PieceColor { Red, Green, Blue, Yellow, Other }
    public class Piece : MonoBehaviour
    {
        [SerializeField] PieceColor color;
        [SerializeField] bool isStatic;
        public bool IsStatic { get => isStatic; }
        public PieceColor Color { get => color; }
        public Vector2Int BoardPosition { get; set; }

    }
}
