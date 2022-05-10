using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGaugeController : MonoBehaviour
{
    private const float posDefault = 0.6155f;
    private const float posMiddle = 0.5f;
    private const float posMaxLeft = 0f;
    private const float posMaxRight = 1f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        var ratio = ScoreCounter.GetScoreRatio();
        var obj = GetComponent<Transform>();
        var xPos = obj.position.x;

        xPos = posDefault * ratio;
        xPos = Mathf.Clamp(xPos, posMaxLeft, posMaxRight);

        //Mathf.Lerp

        //if (xPos > posMaxRight)
        //    xPos = posMaxRight;
        //if (xPos < posMaxLeft)
        //    xPos = posMaxLeft;

        obj.position = new Vector3(xPos, obj.position.y, obj.position.z);
    }
}
