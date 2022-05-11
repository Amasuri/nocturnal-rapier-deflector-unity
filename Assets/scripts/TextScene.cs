using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TextPool;

public class TextScene : MonoBehaviour
{
    private int MaxTextId => CurrentText.Length - 1;
    private int CurrentTextInt = 0;
    private string[] CurrentText = { };
    private string FormattedLine = "";
    public static SceneType CurrentSceneType = SceneType.First;

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

    // Start is called before the first frame update
    private void Start()
    {
        InitNewScene(CurrentSceneType);
        textTerminal = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateTextLine();
    }

    // Update is called once per frame
    private void Update()
    {
        //Text related
        if (MaxTextId <= 0)
            return;

        UpdateTextLine();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CurrentTextInt++;
            CurrentTextMs = 0;
        }

        //Text state related
        currentActor = GetCurrentActor();
        currentActorState = GetCurrentActorState();

        //Scene load
        if (CurrentTextInt >= MaxTextId)
        {
            CorrectTextIDValue();

            if (IsASceneBeforeBattle(CurrentSceneType))
            {
                SceneManager.LoadScene("battle");
                SceneManager.UnloadSceneAsync("dialogue");
            }
            else if (CurrentSceneType == SceneType.PlayerLost)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    CurrentSceneType = LastValidSceneType;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    Application.Quit();
                }
            }
        }

        //In case of other scenes, player will be able get back on track with their playthrought
        if(IsASceneBeforeBattle(CurrentSceneType))
        {
            LastValidSceneType = CurrentSceneType;
        }
    }

    private void UpdateTextLine()
    {
        //Ideally it's corrected in Update cycle. But idk, sometimes Unity doesn't do that. Sometimes the cycle is left with an error before the correction.
        CorrectTextIDValue();

        //Wait for fade in finish
        if (ScreenFadeoutController.IsCurrentlyFading)
            return;

        CurrentTextMs += Time.deltaTime * 1000;
        FormattedLine = TextUtils.GetTimeCharSplitFittedLine(CurrentText[CurrentTextInt], 9999, (int)CurrentTextMs);
        textTerminal.text = FormattedLine;
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
