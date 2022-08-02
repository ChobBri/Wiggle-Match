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
        Vector2Int previousDirectionBuffer = Vector2Int.up;

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
                    previousDirectionBuffer = directionBuffer;
                    moveRateTime = 0.0f;
                } else
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow) && previousDirectionBuffer != Vector2Int.left)
                    {
                        directionBuffer = Vector2Int.right;
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow) && previousDirectionBuffer != Vector2Int.right)
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
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    directionBuffer = Vector2Int.right;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    directionBuffer = Vector2Int.left;
                }
                else
                {
                    directionBuffer = Vector2Int.up;
                }
                moveRateTime = 0.0f;
            }
        }
    }
}
