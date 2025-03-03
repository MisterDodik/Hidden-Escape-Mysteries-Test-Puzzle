using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    bool wasAtCorrectPlace = false;

    [SerializeField] private int winPosition;           // Each circle has its correct position, ie silingIndex

    private void Start()
    {
        GetPosition();
    }
    public int GetPosition()
    {
        int currentPos = transform.GetSiblingIndex();

        if (currentPos == winPosition)
        {
            wasAtCorrectPlace = true;
            return 1;
        }


        // This means that the circle was previously at the correct position which means, it incremented the puzzlemanager.winprogress value by 1 last time
        if (wasAtCorrectPlace)
        {
            wasAtCorrectPlace=false;       
            return -1;
        }
        return 0;
    }

}
