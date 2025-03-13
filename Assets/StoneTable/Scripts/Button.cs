using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private int buttonIndex;

    private void Start()
    {
        buttonIndex = transform.GetSiblingIndex();
    }

    private void OnMouseDown()
    {
        StoneTableManager.instance.Rotate(buttonIndex);
    }
}
