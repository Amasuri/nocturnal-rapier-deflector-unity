using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TextPool;

public class TextScene : MonoBehaviour
{
    private int MaxTextId => CurrentText.Length - 1;
    private int CurrentTextInt = 0;
    public string[] CurrentText = { };
    public SceneType CurrentSceneType;

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

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CurrentTextInt++;
            UpdateTextLine();
        }

        if(CurrentTextInt >= MaxTextId)
        {
            CurrentTextInt = MaxTextId;
        }
    }

    private void UpdateTextLine()
    {
        textTerminal.text = CurrentText[CurrentTextInt];
    }

    public void InitNewScene(SceneType sceneType)
    {
        CurrentText = TextPool.GetPreBattleDialogue(sceneType);
        CurrentTextInt = 0;
    }
}
