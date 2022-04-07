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
            //todo
        }

        string minutes = ((int)TimeLeftSec / 60).ToString();
        string seconds = ((int)TimeLeftSec % 60).ToString();
        descTextTerminal.text = string.Format("Witch: {0}    {2}:{3}    You: {1}", ScoreCounter.WitchHits, ScoreCounter.RapierHits, minutes, seconds);
    }

    public void ResetTimer(bool isBoss = false)
    {
        TimeLeftSec = isBoss ? TimeLimitBossSec : TimeLimitSec;
    }
}
