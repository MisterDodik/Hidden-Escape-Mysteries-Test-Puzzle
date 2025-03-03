using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LightSource")
        {
            gameObject.tag = "LightSource";
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "LightSource")
        {
            gameObject.tag = "Untagged";
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
