using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInPopupController : MonoBehaviour
{
    public Side side;

    private const float updXcoeff = 10f;
    static public bool RapierBoxArrived { get; private set; }
    static public bool WitchBoxArrived { get; private set; }
    public enum Side
    {
        WitchRightUp,
        RapierLeftDown
    }

    // Start is called before the first frame update
    private void Start()
    {
        RapierBoxArrived = false;
        WitchBoxArrived = false;

        if (side == Side.RapierLeftDown)
            transform.position = new Vector3(-4, transform.position.y, transform.position.z);
        else if (side == Side.WitchRightUp)
            transform.position = new Vector3(+4, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        if(side == Side.RapierLeftDown && !RapierBoxArrived)
        {
            transform.position += new Vector3(updXcoeff * Time.deltaTime, 0, 0);
            if(transform.position.x >= 0)
            {
                RapierBoxArrived = true;
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }
        else if (side == Side.WitchRightUp && RapierBoxArrived && !WitchBoxArrived)
        {
            transform.position -= new Vector3(updXcoeff * Time.deltaTime, 0, 0);
            if (transform.position.x <= 0)
            {
                WitchBoxArrived = true;
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }
    }
}
