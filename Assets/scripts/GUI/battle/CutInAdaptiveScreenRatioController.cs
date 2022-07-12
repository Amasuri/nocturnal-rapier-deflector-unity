using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInAdaptiveScreenRatioController : MonoBehaviour
{
    public PopUpPart popUpPart;
    public enum PopUpPart
    {
        Left,
        Right,
        Middle
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Technically this should work adaptive scaling, but alas

        //var cutouts = Screen.cutouts;

        //if (!SystemInfo.operatingSystem.Contains("Android"))
        //    return;

        //switch (popUpPart)
        //{
        //    case PopUpPart.Left:
        //        transform.position.Set(Camera.main.ScreenToWorldPoint(new Vector3(0 - cutouts[0].width, 0,0)).x, transform.position.y, transform.position.z);
        //        break;

        //    case PopUpPart.Right:
        //        var length = gameObject.GetComponent<SpriteRenderer>().size.x;
        //        transform.position.Set(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + cutouts[0].width - length, 0, 0)).x, transform.position.y, transform.position.z);
        //        break;

        //    case PopUpPart.Middle:
        //        transform.localScale = new Vector3(2f,1,1);
        //        break;
        //}
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
