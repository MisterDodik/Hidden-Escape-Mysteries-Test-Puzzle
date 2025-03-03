using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleLogics : MonoBehaviour
{
    GameObject parent;

    OnHexagonClick parentController;
    private void Awake()
    {
        parent = transform.parent.gameObject;
        parentController = parent.GetComponent<OnHexagonClick>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LightSource" && gameObject.tag!="LightSource")
        {
            
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                parent.transform.GetChild(i).gameObject.tag = "LightSource";
            }

            if (parentController)
                parentController.CheckState(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(i).gameObject.tag = "Untagged";
            parentController.CheckState(false);
        }
    }
}
