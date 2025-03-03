using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHexagonClick : MonoBehaviour
{
    private float duration = 0.5f;

    SpriteRenderer spriteRenderer;
    Collider2D selfCollider;

    bool wasCorrect = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selfCollider = GetComponent<Collider2D>();
    }
    private void OnMouseDown()
    {
        StartCoroutine(RotateOverTime());
    }
    IEnumerator RotateOverTime()
    {
        selfCollider.enabled = false;

        Collider2D[] surroundingPaths = Physics2D.OverlapCircleAll(transform.position, 1, 64);
        foreach(Collider2D item in surroundingPaths)
        {
            item.enabled = false;
            item.gameObject.GetComponent<SpriteRenderer>().enabled=false;
        }


        float elapsedTime = 0f;   

        Quaternion currentRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, currentRotation.eulerAngles.z - 60);

        while (elapsedTime < duration)
        {      
            transform.localRotation = Quaternion.Lerp(currentRotation, endRotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = endRotation;


        selfCollider.enabled = true;

        foreach (BoxCollider2D item in surroundingPaths)
        {
            item.enabled = true;
        }
    }
    public void CheckState(bool isCorrect)
    {
        if (isCorrect)
        {
            spriteRenderer.sprite = HexagonPuzzleManager.instance.hexagonShineSprite;

            if(!wasCorrect)
                HexagonPuzzleManager.instance.checkWin(1);
            wasCorrect = true;
        }
        else
        {
            spriteRenderer.sprite = HexagonPuzzleManager.instance.defaultHexagonSprite;

            if(wasCorrect)
                HexagonPuzzleManager.instance.checkWin(-1);
            wasCorrect = false;
        }
    }
}
