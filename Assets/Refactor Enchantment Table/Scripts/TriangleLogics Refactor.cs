using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TriangleLogicsRefactor : MonoBehaviour
{
    public bool isLightSource = false;

    public bool isCenter = false;
    TriangleLogicsRefactor[] siblings;
    HexagonScriptRefactor parentController;
    private void Start()
    {
        siblings = transform.parent.GetComponentsInChildren<TriangleLogicsRefactor>();
        parentController = transform.parent.GetComponent<HexagonScriptRefactor>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (EnchantmentTableManagerRefactor.instance.moveInProgress || isLightSource)   // isLightSource check to improve performance by reducing the number of calls
            return;

        // Similar to the original version
        TriangleLogicsRefactor triangle = collision.gameObject.GetComponent<TriangleLogicsRefactor>();
        if (triangle != null && triangle.isLightSource)       
        {
            for (int i = 0; i < siblings.Length; i++)
            {
                siblings[i].isLightSource = true;
            }
            if (parentController)
                parentController.CheckState(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isCenter)       // The center one should always be a light source
            return;

        for (int i = 0; i < siblings.Length; i++)
        {
            siblings[i].isLightSource = false;
        }
        if (parentController)
            parentController.CheckState(false);

    }
}
