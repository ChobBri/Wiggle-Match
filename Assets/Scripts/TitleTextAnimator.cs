using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleTextAnimator : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        text.textInfo.characterInfo[0].bottomLeft = text.textInfo.characterInfo[0].bottomLeft + 20.0f * Vector3.down;
    }
}
