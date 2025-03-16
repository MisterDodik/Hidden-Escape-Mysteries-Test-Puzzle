using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScriptRefactor : MonoBehaviour
{
    TriangleLogicsRefactor currentTriangle;
    public bool wait;
    public bool isPathLightSource = false;

    public bool test = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        TriangleLogicsRefactor triangle = collision.gameObject.GetComponent<TriangleLogicsRefactor>();
        //if (test && triangle!=null)
        //    print((triangle.isLightSource, wait));   
        if (triangle != null) // && triangle.isLightSource // && currentTriangle!=triangle
        {
            isPathLightSource = triangle.isLightSource;
            currentTriangle = triangle;
            //triangle.isLightSource = true;
            GetComponent<SpriteRenderer>().enabled = triangle.isLightSource;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(currentTriangle)
        //    currentTriangle.isLightSource = false;
        currentTriangle = null;
        GetComponent<SpriteRenderer>().enabled = false;
        isPathLightSource = false;
    }
    //public void CustomOnTriggerExit2D()
    //{
    //    //if(currentTriangle)
    //    //    currentTriangle.isLightSource = false;
    //    currentTriangle = null;
    //    GetComponent<SpriteRenderer>().enabled = false;
    //    isPathLightSource = false;
    //}
}
