using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public enum PieceColor { Red, Green, Blue, Yellow, Other }
    public class Piece : MonoBehaviour
    {
        [SerializeField] PieceDestroyEffect destroyFx;
        [SerializeField] PieceColor color;
        [SerializeField] bool isStatic;
        public Vector2 velocity { get; set; } = Vector2.zero;
        public bool IsStatic { get => isStatic; }
        public PieceColor Color { get => color; }
        public Vector2Int BoardPosition { get; set; }

        public void Die()
        {
            PieceDestroyEffect fx = Instantiate(destroyFx, transform.position, Quaternion.identity);
            fx.Init(GetComponent<SpriteRenderer>().sprite);
            Destroy(gameObject);
        }
    }
}
