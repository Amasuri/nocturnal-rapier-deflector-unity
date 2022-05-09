using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public RelativePosition relativePosition = RelativePosition.Center;
    public RelativeDepth relativeDepth = RelativeDepth.Base;
    public enum RelativePosition
    {
        Center,
        Left,
        Right
    }

    public enum RelativeDepth
    {
        Base,
        Parallax1,
        Parallax2
    }

    private readonly float ScrollSpeedBase = 0.25f;
    private readonly float ScrollSpeedParallax1 = 0.15f;
    private readonly float ScrollSpeedParallax2 = 0.05f;

    public SpriteRenderer rend;

    private float lBound;
    private float rBound;

    // Start is called before the first frame update
    private void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();

        //Get different image if loading on different battle scene
        //Scene 1 Parallax 1-3
        if (BattleControl.CurrentBattleType == BattleControl.BattleType.First && relativeDepth == RelativeDepth.Base)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background0");
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.First && relativeDepth == RelativeDepth.Parallax1)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background0_1");
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.First && relativeDepth == RelativeDepth.Parallax2)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background0_2");

        //Scene 2 Parallax 1-3
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.Second && relativeDepth == RelativeDepth.Base)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background1");
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.Second && relativeDepth == RelativeDepth.Parallax1)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background1_1");
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.Second && relativeDepth == RelativeDepth.Parallax2)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background1_2");

        //Scene 3 Parallax 1-3
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.ThirdBoss && relativeDepth == RelativeDepth.Base)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background2");
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.ThirdBoss && relativeDepth == RelativeDepth.Parallax1)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background2_1");
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.ThirdBoss && relativeDepth == RelativeDepth.Parallax2)
            rend.sprite = Resources.Load<Sprite>("sprite/bg/background2_2");

        lBound = 0f;
        rBound = rend.bounds.size.x;

        if (relativePosition == RelativePosition.Center)
        {
            lBound = 0f;
            rBound = rend.bounds.size.x;
        }
        else if (relativePosition == RelativePosition.Left)
        {
            lBound = -rend.bounds.size.x;
            rBound = 0f;
        }
        else if (relativePosition == RelativePosition.Right)
        {
            lBound = rend.bounds.size.x;
            rBound = rend.bounds.size.x * 2;
        }

        transform.position = new Vector2(lBound, transform.position.y);
    }

    // Update is called once per frame
    private void Update()
    {
        var scrollspd = ScrollSpeedBase;
        if (relativeDepth == RelativeDepth.Parallax1)
            scrollspd = ScrollSpeedParallax1;
        else if (relativeDepth == RelativeDepth.Parallax2)
            scrollspd = ScrollSpeedParallax2;

        //Moving base position
        var movement = new Vector2(scrollspd, 0) * Time.deltaTime;
        transform.Translate(movement);

        if (transform.position.x > rBound)
            transform.position = new Vector2(lBound, transform.position.y);

        //Debug.Log(Convert.ToString(transform.position.x) + " " + Convert.ToString(img.size.x));
    }
}
