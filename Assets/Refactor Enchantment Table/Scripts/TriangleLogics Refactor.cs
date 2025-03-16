using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleLogicsRefactor : MonoBehaviour
{
     public bool isLightSource = false;
    public bool test = false;
    public bool wait = false;

    public bool isCenter = false;
    TriangleLogicsRefactor[] siblings;
    HexagonScriptRefactor parentController;
    private void Start()
    {
        siblings = transform.parent.GetComponentsInChildren<TriangleLogicsRefactor>();
        parentController = transform.parent.GetComponent<HexagonScriptRefactor>();
    }
    private void Update()
    {
        //if(test)
        //    if (!isLightSource)
        //        print("ima nade");
        if (test)
            print(isLightSource);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PathScriptRefactor path = collision.gameObject.GetComponent<PathScriptRefactor>();
        if (path != null && path.isPathLightSource && !isLightSource) // && currentTriangle!=triangle
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
        if (isCenter)
            return;

        PathScriptRefactor path = collision.gameObject.GetComponent<PathScriptRefactor>();
       
        //if (path)         //ovo ne treba jer se uopste pravi path ne uzima u obzir jer se ne pokrene ova funkcija na tacnom triangleu
        //{
        //   // path.CustomOnTriggerExit2D();
        //   path.wait = true;
        //}
        for (int i = 0; i < siblings.Length; i++)
        {
            siblings[i].isLightSource = false;
            if(test)
                print("cao");
        }
        if (parentController)
            parentController.CheckState(false);
        //if (path)
        //{
        //    // path.CustomOnTriggerExit2D();
        //    path.wait = false;
        //}
    }
}
