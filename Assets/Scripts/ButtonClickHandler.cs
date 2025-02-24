using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    private int buttonIndex;

    private void Start()
    {
        buttonIndex = transform.GetSiblingIndex();
    }

    private void OnMouseDown()
    {
        PuzzleManager.instance.Rotate(buttonIndex);
    }
}
