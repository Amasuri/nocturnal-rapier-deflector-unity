using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float ScrollSpeed = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //Moving base position
        var movement = new Vector2(ScrollSpeed, 0) * Time.deltaTime;
        transform.Translate(movement);

        var img = gameObject.GetComponent<SpriteRenderer>();
        if (transform.position.x > img.size.x)
            transform.position = new Vector2(0, transform.position.y);

        Debug.Log(Convert.ToString(transform.position.x) + " " + Convert.ToString(img.size.x));
    }
}
