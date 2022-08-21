using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    // REF
    public GameObject helperCanvas;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            helperCanvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            helperCanvas.SetActive(false);
        }
    }
}
