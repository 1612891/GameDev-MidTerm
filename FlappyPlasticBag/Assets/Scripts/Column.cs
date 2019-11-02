using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<bird>() != null)
        {
            Debug.Log("All of communist will be dead " + collision.name);
            GameControl.instance.BirdScore();
            GameControl.instance.isExit = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<bird>() != null)
        {
            GameControl.instance.isExit = true;
        }
    }
}
