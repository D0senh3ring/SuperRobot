using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteScroll : MonoBehaviour
{
    [SerializeField]
    private float horizontalScrollSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 offset = Vector2.right * Time.deltaTime * this.horizontalScrollSpeed;
        this.spriteRenderer.size += offset;
        this.transform.Translate(-offset.x * 2, 0, 0);
    }
}
