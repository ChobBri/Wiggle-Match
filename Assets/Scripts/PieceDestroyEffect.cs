using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDestroyEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= Vector3.one * 5.0f * Time.deltaTime;
        if (transform.localScale.x <= 0.0f) Destroy(gameObject); 
    }
}
