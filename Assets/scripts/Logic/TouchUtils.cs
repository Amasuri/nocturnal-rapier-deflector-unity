using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUtils : MonoBehaviour
{
    /// <summary>
    /// Returns true only on handhelds. Checks if double-tap happened.
    /// </summary>
    static public bool IsDoubleTap()
    {
        bool isMobile = SystemInfo.deviceType == DeviceType.Handheld;
        bool isDoubleTap = false;
        if (isMobile && Input.GetTouch(0).tapCount == 2)
            isDoubleTap = true;

        return isDoubleTap;
    }
}
