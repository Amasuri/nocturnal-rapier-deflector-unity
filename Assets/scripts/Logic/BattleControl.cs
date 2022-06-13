using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleControl : MonoBehaviour
{
    private const float TimeLimitSecFirst = 70;
    private const float TimeLimitSecSecond = 90;
    private const float TimeLimitSecBoss = 120;
    public float TimeLeftSec { get; private set; }

    private bool HasFinishedBattle = false;

    public static float TimeLeftSecLast { get; private set; }

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
        if (CutInOverlayController.CutInEnabled && !HasFinishedBattle)
            return;

        if(HasFinishedBattle)
        {
            if (!CutInOverlayController.CutInEnabled)
                RememberScoreAndSwitchToDialogueScene();
            return;
        }

        TimeLeftSec -= Time.deltaTime;
        if(TimeLeftSec <= 0f)
        {
            CutInOverlayController.IsAtBattleStart = false;
            CutInOverlayController.refToCutInOverlay.SetActive(true);
            CutInOverlayController.refToCutInOverlay.GetComponent<CutInOverlayController>().Reload();

            HasFinishedBattle = true;
        }

        //Time & Score line
        descTextTerminal.text = string.Format("{0}                      {1}", ScoreCounter.WitchTookHits.ToString("000"), ScoreCounter.RapierTookHits.ToString("000"));

        //Winning conditions line
        //DEBUG
        //descTextTerminal.text += string.Format("\nRatio: {0}", ScoreCounter.GetScoreRatio().ToString("0.00"));

        TimeLeftSecLast = TimeLeftSec;
    }

    private static void RememberScoreAndSwitchToDialogueScene()
    {
        if (ScoreCounter.GetIsPlayerWinning())
        {
            ScoreCounter.RememberCurrentScores();

            if (TextScene.CurrentSceneType == TextPool.SceneType.First || TextScene.CurrentSceneType == TextPool.SceneType.Second || TextScene.CurrentSceneType == TextPool.SceneType.ThirdBoss)
                TextScene.CurrentSceneType++;

            if (BattleControl.CurrentBattleType < BattleType.ThirdBoss)
                UpdateCurrentBattleTypeToSceneType();

            SceneManager.LoadScene("dialogue");
            SceneManager.UnloadSceneAsync("battle");
        }
        else
        {
            TextScene.CurrentSceneType = TextPool.SceneType.PlayerLost;

            ScoreCounter.IncreaseRetries();

            SceneManager.LoadScene("dialogue");
            SceneManager.UnloadSceneAsync("battle");
        }
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
        HasFinishedBattle = false;

        if (CurrentBattleType == BattleType.First)
            TimeLeftSec = TimeLimitSecFirst;
        else if (CurrentBattleType == BattleType.Second)
            TimeLeftSec = TimeLimitSecSecond;
        else if (CurrentBattleType == BattleType.ThirdBoss)
            TimeLeftSec = TimeLimitSecBoss;
    }
}
