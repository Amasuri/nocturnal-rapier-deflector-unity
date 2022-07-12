using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreWarningController : MonoBehaviour
{
    public SpriteRenderer render;

    public Sprite alertYellow;
    public Sprite alertWhite;

    // Start is called before the first frame update
    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        var isWinning = ScoreCounter.GetIsPlayerWinning();
        var lowTime = BattleControl.TimeLeftSecLast <= 30f;

        //Disable or enable render on winning condition
        if (isWinning && render.enabled)
        {
            render.enabled = false;
        }
        else if(!isWinning && !render.enabled)
        {
            render.enabled = true;
        }

        //Flash if on low time
        if(render.enabled && lowTime)
        {
            if ((int)BattleControl.TimeLeftSecLast % 2 == 1)
                render.sprite = alertWhite;
            else
                render.sprite = alertYellow;
        }
    }
}
