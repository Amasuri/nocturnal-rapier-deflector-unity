using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleControl : MonoBehaviour
{
    private const float TimeLimitSec = 90;
    private const float TimeLimitBossSec = 120;
    public float TimeLeftSec { get; private set; }

    public TextMeshProUGUI descTextTerminal;

    // Start is called before the first frame update
    private void Start()
    {
        descTextTerminal = gameObject.GetComponent<TextMeshProUGUI>();
        ResetTimer();
    }

    // Update is called once per frame
    private void Update()
    {
        TimeLeftSec -= Time.deltaTime;
        if(TimeLeftSec <= 0f)
        {
            if(ScoreCounter.GetIsPlayerWinning())
            {
                //todo
            }
            else
            {
                //todo
            }
        }

        //Time & Score line
        string minutes = ((int)TimeLeftSec / 60).ToString();
        string seconds = ((int)TimeLeftSec % 60).ToString();
        descTextTerminal.text = string.Format("Witch: {0}    {2}:{3}    You: {1}", ScoreCounter.WitchTookHits, ScoreCounter.RapierTookHits, minutes, seconds);

        //Winning conditions line
        var append = ScoreCounter.GetIsPlayerWinning() ? "Winning!" : "Loosing...";
        descTextTerminal.text += string.Format("\nRatio: {0} {1}", ScoreCounter.GetScoreRatio().ToString("0.00"), append);
    }

    public void ResetTimer(bool isBoss = false)
    {
        TimeLeftSec = isBoss ? TimeLimitBossSec : TimeLimitSec;
    }
}
