using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;

    // Start is called before the first frame update
    private void Awake()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.x;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x < -groundHorizontalLength)
        {
            RepositionBackGround();
        }
    }

    private void RepositionBackGround()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
