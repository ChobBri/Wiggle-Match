using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public class PieceSet
    {
        public Piece[] Pieces { get; protected set; }

        public PieceSet(Piece[] pieces)
        {
            Pieces = pieces;
        }
    }
}
