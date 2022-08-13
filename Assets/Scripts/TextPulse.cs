using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPulse : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] float sizeOffset = 10.0f;
    [SerializeField] float speed = 2.0f;

    float startingSize;
    float t = 0.0f;

    private void Awake()
    {
        startingSize = text.fontSize;
    }
   
    private void Update()
    {
        t += speed * Time.deltaTime;
        if (t >= Mathf.PI * 2.0f) t -= Mathf.PI * 2.0f;
        text.fontSize = Mathf.Sin(t + Mathf.PI) * sizeOffset + startingSize;
    }
}
