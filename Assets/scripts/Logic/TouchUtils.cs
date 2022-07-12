using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUtils : MonoBehaviour
{
    static private bool hadDoubleTapRecently;
    private const float msMaxDoubleTapDelay = 0.3f;
    static private float msDoubleTapDelay;

    static public bool IsXLeftPart(float x) => x < Screen.width / 2;

    static public bool IsXRightPart(float x) => x >= Screen.width / 2;

    private void Start()
    {
        hadDoubleTapRecently = false;
        msDoubleTapDelay = 0f;
    }

    private void FixedUpdate()
    {
        if(msDoubleTapDelay > 0f)
        {
            msDoubleTapDelay -= Time.deltaTime;
            if (msDoubleTapDelay <= 0f)
                hadDoubleTapRecently = false;
        }
    }

    /// <summary>
    /// Returns true only on handhelds (& possibly Mac OS). Checks if double-tap happened.
    /// </summary>
    static public bool IsDoubleTap()
    {
        if (Input.touchCount <= 0)
            return false;

        //Originally intended to be handhelds only, but i'm thinking of Mac OS port too. Need to debug that later
        //to see if Mac OS supports the same API
        bool isMobile = true;
        bool isDoubleTap = false;
        if (isMobile && Input.GetTouch(0).tapCount == 2)
        {
            isDoubleTap = true;
        }

        return isDoubleTap;
    }

    static public Touch GetRightScreenTouch()
    {
        var touches = Input.touches;
        var defaultTouch = Input.GetTouch(0); //default
        foreach (var t in touches)
        {
            if (IsXRightPart(t.position.x))
                return t;
        }

        return defaultTouch;
    }

    static public Touch GetLeftScreenTouch()
    {
        var touches = Input.touches;
        var defaultTouch = Input.GetTouch(0); //default
        foreach (var t in touches)
        {
            if (IsXLeftPart(t.position.x))
                return t;
        }

        return defaultTouch;
    }

    /// <summary>
    /// Same as IsDoubleTap, but time-restricted
    /// </summary>
    static public bool IsUniqueDoubleTap()
    {
        var tap = false;
        if (!hadDoubleTapRecently)
        {
            tap = IsDoubleTap();
            if(tap)
            {
                hadDoubleTapRecently = true;
                msDoubleTapDelay = msMaxDoubleTapDelay;
            }
        }

        return tap;
    }
}
