using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGaugeController : MonoBehaviour
{
    private const float posDefaultWin = 0.6155f;
    private const float posMiddle = 0.5f;
    private const float posMaxLeft = 0f;
    private const float posMaxRight = 1f;

    private float xPos;

    private SpriteRenderer img;
    private Vector2 origSize;

    // Start is called before the first frame update
    private void Start()
    {
        img = GetComponent<SpriteRenderer>();
        origSize = img.size;
    }

    // Update is called once per frame
    private void Update()
    {
        ChangeGaugePosByRatio();

        img.size = new Vector2(origSize.x * (posMaxRight - xPos), origSize.y);

        //tex.Resize(origSizeX, tex.height); //(int)(origSizeX * (1f - xPos))
    }

    private void ChangeGaugePosByRatio()
    {
        var ratio = ScoreCounter.GetScoreRatio();
        var obj = GetComponent<Transform>();
        xPos = obj.position.x;

        xPos = posDefaultWin * ratio;

        //Making the gauge appear less dramatic
        if (xPos > posDefaultWin)
            xPos = posDefaultWin + xPos / 10;

        //if (xPos < posDefaultWin)
        //    xPos = posDefaultWin - xPos / 10;

        xPos = Mathf.Clamp(xPos, posMaxLeft, posMaxRight);

        obj.position = new Vector3(xPos, obj.position.y, obj.position.z);
    }
}
