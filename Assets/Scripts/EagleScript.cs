using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    PathFollower pathFollower;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        pathFollower = GetComponent<PathFollower>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if ((pathFollower.currentPoint.transform.position - transform.position).x > 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
