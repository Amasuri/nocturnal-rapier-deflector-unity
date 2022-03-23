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
    public SceneType CurrentSceneType;
    private float CurrentTextMs = 0f;

    public TextMeshProUGUI textTerminal;

    // Start is called before the first frame update
    private void Start()
    {
        InitNewScene(SceneType.First);
        textTerminal = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateTextLine();
    }

    // Update is called once per frame
    private void Update()
    {
        if (MaxTextId <= 0)
            return;

        UpdateTextLine();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CurrentTextInt++;
            CurrentTextMs = 0;
        }

        if(CurrentTextInt >= MaxTextId)
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
}
