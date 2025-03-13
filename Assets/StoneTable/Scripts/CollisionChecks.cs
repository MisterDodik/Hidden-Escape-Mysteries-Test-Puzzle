using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecks : MonoBehaviour
{
    Button currentHover;

    SpriteRenderer spriteRenderer;
    Sprite glowSprite;
    Sprite beamSprite;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        glowSprite = StoneTableManager.instance.data.glowSprite;
        beamSprite = StoneTableManager.instance.data.beamSprite;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("evo ga");
        Button button = collision.gameObject.GetComponent<Button>();
        if (button != null)
        {
            currentHover = button;
            spriteRenderer.sprite = beamSprite;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Button button = collision.gameObject.GetComponent<Button>();
        if (button == currentHover)
        {
            currentHover = null;
            spriteRenderer.sprite = glowSprite;
        }

    }
}
