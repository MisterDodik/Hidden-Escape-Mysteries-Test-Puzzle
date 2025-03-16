using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScriptRefactor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TriangleLogicsRefactor triangle = collision.gameObject.GetComponent<TriangleLogicsRefactor>();
        if (triangle != null && spriteRenderer.enabled!=triangle.isLightSource)
        {
            spriteRenderer.enabled = triangle.isLightSource;
        }
    }
}
