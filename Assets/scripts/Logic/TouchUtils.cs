using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUtils : MonoBehaviour
{
    /// <summary>
    /// Returns true only on handhelds (& possibly Mac OS). Checks if double-tap happened.
    /// </summary>
    static public bool IsDoubleTap()
    {
        //Originally intended to be handhelds only, but i'm thinking of Mac OS port too. Need to debug that later
        //to see if Mac OS supports the same API
        bool isMobile = true;
        bool isDoubleTap = false;
        if (isMobile && Input.GetTouch(0).tapCount == 2)
            isDoubleTap = true;

        return isDoubleTap;
    }
}
