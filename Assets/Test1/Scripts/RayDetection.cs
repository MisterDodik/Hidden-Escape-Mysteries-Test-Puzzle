using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDetection : MonoBehaviour
{
    private Sprite glow;
    private Sprite beam;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        glow = PuzzleManager.instance.glowSprite;
        beam = PuzzleManager.instance.beamSprite;

        spriteRenderer = GetComponent<SpriteRenderer>();
        testRay();
    }
    public void testRay()
    {
        float angle = transform.localEulerAngles.z + transform.parent.localEulerAngles.z;
        angle *= Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        if (Physics2D.Raycast(transform.position, direction, 3))
        {
            spriteRenderer.sprite = beam;
        }
        else
        {
            spriteRenderer.sprite = glow;
        }
    }
}
