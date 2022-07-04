using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using static TextPool;

public class TextScene : MonoBehaviour
{
    private int MaxTextId => CurrentText.Length - 1;
    private int CurrentTextInt = 0;
    private string[] CurrentText = { };
    private string FormattedLine = "";
    public static SceneType CurrentSceneType = SceneType.First;

    public bool TutorialIsShowingLeftSide => CurrentTextInt >= 17 && CurrentTextInt <= 18;
    public bool TutorialIsShowingRightSide => CurrentTextInt >= 14 && CurrentTextInt <= 16 || CurrentTextInt >= 19 && CurrentTextInt <= 21;

    static public TextScene current;

    /// <summary>
    /// In case of other scenes, player will be able get back on track with their playthrought
    /// </summary>
    public static SceneType LastValidSceneType = SceneType.First;
    private float CurrentTextMs = 0f;

    public TextMeshProUGUI textTerminal;

    public static CurrentActor currentActor;
    public static CurrentActorState currentActorState;

    public enum CurrentActor
    {
        Witch,
        Rapier
    }
    public enum CurrentActorState
    {
        Normal,
        Talking,
        Shocked
    }

    private void Start()
    {
        current = this;

        InitNewScene(CurrentSceneType);
        textTerminal = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateTextLine();
    }

    private void Update()
    {
        //In case if somehow async load postpones too much, finish ad there
        Advertisement.Banner.Hide();

        //Text bug related
        if (MaxTextId <= 0)
            return;

        //This line
        UpdateTextLine();
        CheckForNextLineAction();

        //Text state related
        currentActor = GetCurrentActor();
        currentActorState = GetCurrentActorState();

        //Scene load
        CheckForSceneLoadOnLastLine();

        //In case of other scenes, player will be able get back on track with their playthrought
        if (IsASceneBeforeBattle(CurrentSceneType))
        {
            LastValidSceneType = CurrentSceneType;
        }
    }

    private void CheckForSceneLoadOnLastLine()
    {
        if (CurrentTextInt >= MaxTextId)
        {
            CorrectTextIDValue();

            if (IsASceneBeforeBattle(CurrentSceneType))
            {
                SceneManager.LoadScene("battle");
                SceneManager.UnloadSceneAsync("dialogue");
                CutInOverlayController.IsAtBattleStart = true;
            }
            else if (CurrentSceneType == SceneType.PlayerLost)
            {
                if (Input.GetKeyDown(KeyCode.Return) || TouchUtils.IsDoubleTap())
                {
                    CurrentSceneType = LastValidSceneType;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    Application.Quit();
                }
            }
            else if (CurrentSceneType == SceneType.WonGame)
            {
                SceneManager.LoadScene("credits");
                SceneManager.UnloadSceneAsync("dialogue");
            }
        }
    }

    /// <summary>
    /// Check for LMB or Space to advance to next line. Which also works as line length maximizer, if line isn't full length yet.
    /// </summary>
    private void CheckForNextLineAction()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            var maxText = TextUtils.GetTimeCharSplitFittedLine(CurrentText[CurrentTextInt], 9999, 100000);
            var currText = textTerminal.text;

            //If not all text displayed yet, display all text. Else, click to next slide
            if (currText.Length >= maxText.Length - 3)
            {
                CurrentTextInt++;
                CurrentTextMs = 0;
            }
            else
            {
                CurrentTextMs = 100000;
            }
        }
    }

    /// <summary>
    /// Since lines are dynamical, each line is updated every tick. Mainly for timed text & text data correction
    /// </summary>
    private void UpdateTextLine()
    {
        //Ideally it's corrected in Update cycle. But idk, sometimes Unity doesn't do that. Sometimes the cycle is left with an error before the correction.
        CorrectTextIDValue();

        //Wait for fade in finish
        if (ScreenFadeoutController.IsCurrentlyFading)
            return;

        CurrentTextMs += Time.deltaTime * 1000;
        FormattedLine = TextUtils.GetTimeCharSplitFittedLine(CurrentText[CurrentTextInt], 9999, (int)CurrentTextMs);

        if (FormattedLine.Length > 3)
            textTerminal.text = FormattedLine.Substring(3);
        else
            textTerminal.text = "";
    }

    private void CorrectTextIDValue()
    {
        if (CurrentTextInt >= MaxTextId)
            CurrentTextInt = MaxTextId;
    }

    public void InitNewScene(SceneType sceneType)
    {
        CurrentText = TextPool.GetDialogue(sceneType);
        CurrentTextInt = 0;
    }

    public CurrentActorState GetCurrentActorState()
    {
        //On the left is raw data of current line. On the right is formatted & time-encoded speech (which means, cut mid-sentence if actor is speaking)
        if (CurrentText[CurrentTextInt].Length > FormattedLine.Length && !ScreenFadeoutController.IsCurrentlyFading)
            return CurrentActorState.Talking;
        else
            return CurrentActorState.Normal;
    }

    public CurrentActor GetCurrentActor()
    {
        if (CurrentText[CurrentTextInt][0] == 'W')
            return CurrentActor.Witch;
        else
            return CurrentActor.Rapier;
    }

    static public bool IsASceneBeforeBattle(SceneType scene)
    {
        return scene == SceneType.First || scene == SceneType.Second || scene == SceneType.ThirdBoss;
    }
}
