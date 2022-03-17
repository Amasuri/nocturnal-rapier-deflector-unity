using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public RelativePosition relativePosition = RelativePosition.Center;
    public enum RelativePosition
    {
        Center,
        Left,
        Right
    }

    public float ScrollSpeed = 0.15f;

    private float lBound;
    private float rBound;

    // Start is called before the first frame update
    private void Start()
    {
        var img = gameObject.GetComponent<SpriteRenderer>();

        lBound = 0f;
        rBound = img.bounds.size.x;

        if (relativePosition == RelativePosition.Center)
        {
            lBound = 0f;
            rBound = img.bounds.size.x;
        }
        else if (relativePosition == RelativePosition.Left)
        {
            lBound = -img.bounds.size.x;
            rBound = 0f;
        }
        else if (relativePosition == RelativePosition.Right)
        {
            lBound = img.bounds.size.x;
            rBound = img.bounds.size.x * 2;
        }

        transform.position = new Vector2(lBound, transform.position.y);
    }

    // Update is called once per frame
    private void Update()
    {
        //Moving base position
        var movement = new Vector2(ScrollSpeed, 0) * Time.deltaTime;
        transform.Translate(movement);

        if (transform.position.x > rBound)
            transform.position = new Vector2(lBound, transform.position.y);

        //Debug.Log(Convert.ToString(transform.position.x) + " " + Convert.ToString(img.size.x));
    }
}
