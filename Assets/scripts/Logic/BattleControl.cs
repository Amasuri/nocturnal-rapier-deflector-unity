using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleControl : MonoBehaviour
{
    private const float TimeLimitSec = 90;
    private const float TimeLimitBossSec = 120;
    public float TimeLeftSec { get; private set; }

    public TextMeshProUGUI descTextTerminal;

    /// <summary>
    /// Various battles has differing: 1. attacks patterns 2. backgrounds 3. music 4. time limits
    /// </summary>
    public static BattleType CurrentBattleType = BattleType.First;

    public enum BattleType
    {
        First,
        Second,
        ThirdBoss
    }

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
                if(TextScene.CurrentSceneType == TextPool.SceneType.First || TextScene.CurrentSceneType == TextPool.SceneType.Second || TextScene.CurrentSceneType == TextPool.SceneType.ThirdBoss)
                    TextScene.CurrentSceneType++;

                if (BattleControl.CurrentBattleType < BattleType.ThirdBoss)
                    UpdateCurrentBattleTypeToSceneType();

                SceneManager.LoadScene("dialogue");
                SceneManager.UnloadSceneAsync("battle");
            }
            else
            {
                TextScene.CurrentSceneType = TextPool.SceneType.PlayerLost;

                SceneManager.LoadScene("dialogue");
                SceneManager.UnloadSceneAsync("battle");
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

    private static void UpdateCurrentBattleTypeToSceneType()
    {
        if (TextScene.CurrentSceneType == TextPool.SceneType.First)
            BattleControl.CurrentBattleType = BattleType.First;
        else if (TextScene.CurrentSceneType == TextPool.SceneType.Second)
            BattleControl.CurrentBattleType = BattleType.Second;
        else if (TextScene.CurrentSceneType == TextPool.SceneType.ThirdBoss)
            BattleControl.CurrentBattleType = BattleType.ThirdBoss;
    }

    public void ResetTimer()
    {
        var isBoss = CurrentBattleType == BattleType.ThirdBoss ? true : false;

        TimeLeftSec = isBoss ? TimeLimitBossSec : TimeLimitSec;
    }
}
