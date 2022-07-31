using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZL.Movement;

namespace PZL.Controls
{
    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] float moveRate = 0.5f;

        float moveRateTime = 0.0f;

        Vector2Int directionBuffer = Vector2Int.up;

        PieceSetMover mover;

        private void Awake()
        {
            mover = GetComponent<PieceSetMover>();
        }

        private void Update()
        {
            if (mover.HasPieceSet)
            {
                if(moveRateTime >= moveRate)
                {
                    mover.Move(directionBuffer);
                    moveRateTime = 0.0f;
                } else
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        directionBuffer = Vector2Int.right;
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        directionBuffer = Vector2Int.left;
                    } else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        directionBuffer = Vector2Int.up;
                    }

                    moveRateTime += Time.deltaTime;
                }
            } else
            {
                directionBuffer = Vector2Int.up;
                moveRateTime = 0.0f;
            }
        }
    }
}
