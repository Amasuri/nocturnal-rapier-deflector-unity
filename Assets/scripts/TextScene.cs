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
            CurrentTextInt = MaxTextId;
            SceneManager.LoadScene("battle");
            SceneManager.UnloadSceneAsync("dialogue");
        }
    }

    private void UpdateTextLine()
    {
        CurrentTextMs += Time.deltaTime * 1000;
        FormattedLine = TextUtils.GetTimeCharSplitFittedLine(CurrentText[CurrentTextInt], 9999, (int)CurrentTextMs);
        textTerminal.text = FormattedLine;
    }

    public void InitNewScene(SceneType sceneType)
    {
        CurrentText = TextPool.GetPreBattleDialogue(sceneType);
        CurrentTextInt = 0;
    }

    public CurrentActorState GetCurrentActorState()
    {
        //On the left is raw data of current line. On the right is formatted & time-encoded speech (which means, cut mid-sentence if actor is speaking)
        if (CurrentText[CurrentTextInt].Length > FormattedLine.Length)
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
}
