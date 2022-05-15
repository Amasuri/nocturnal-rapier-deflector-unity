using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsTextController : MonoBehaviour
{
    public TextMeshProUGUI textTerminal;

    private float textStepSec;
    private const float textStepSecMax = 0.1f;

    // Start is called before the first frame update
    private void Start()
    {
        textTerminal = gameObject.GetComponent<TextMeshProUGUI>();
        textTerminal.text = GetCreditsText();
        textStepSec = textStepSecMax;
    }

    // Update is called once per frame
    private void Update()
    {
        //textStepSec -= Time.deltaTime;
        //if (textStepSec <= 0f)
        //{
        //    textStepSec = textStepSecMax;

        //    var pos = transform.position;
        //    var scroll = 0.005f;//0.02f * Time.deltaTime;
        //    transform.position = new Vector3(pos.x, pos.y + scroll, pos.z);
        //}

        var pos = transform.position;
        var scroll = 0.04f * Time.deltaTime;

        if(pos.y <= -0.1f)
            transform.position = new Vector3(pos.x, pos.y + scroll, pos.z);
    }

    private string GetCreditsText()
    {
        return
            "\nCode & Concept:" +
            "\n   @AMasumari (Twitter)" +
            "\nAwesome Music:" +
            "\n   @domainofeyes (Twitter)" +
            "\nAwesome Art:" +
            "\n   @YoukaiDrawing (Twitter)" +
            "\n" +
            "\nJuicy Sounds:" +
            "\n   Various guys at freesound.org:" +
            "\n   johnnyguitar01: single-firework" +
            "\n   tommccann: explosion-01" +
            "\n   johnbuhr: sword-clash-3" +
            "\n   qubodup: sword-hit" +
            "\n   misscellany: metallic-whoosh" +
            "\n   jamesabdulrahman: unsheathed-blade" +
            "\n   sypherzent: cut-through-armor-slice-clang" +
            "\n" +
            "\nUnity port & remake of a game made during " +
            "\n\"3 weeks 3 games\" challenge on Monogame," +
            "\noriginal by the same team." +
            "\n" +
            "\nMade with love by @AMasumari and their friends <3" +
            "\n" +
            "\nThanks for playing!" +
            "\n(Click for your score or check the score.txt)";
    }

    private string GetScoreText()
    {
        return "";
    }
}
