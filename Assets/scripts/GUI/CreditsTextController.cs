using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsTextController : MonoBehaviour
{
    public TextMeshProUGUI textTerminal;

    private float textStepSec;
    private const float textStepSecMax = 0.1f;
    private float originalY;

    // Start is called before the first frame update
    private void Start()
    {
        textTerminal = gameObject.GetComponent<TextMeshProUGUI>();
        textTerminal.text = GetCreditsText();
        textStepSec = textStepSecMax;

        originalY = transform.position.y;
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

        if(Input.GetMouseButtonDown(0))
        {
            textTerminal.text = GetScoreText();
            transform.position = new Vector3(pos.x, originalY, pos.z);
        }
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
            "\n(Click for your score or check the score.txt)\n";
    }

    private string GetScoreText()
    {
        //Took means took hit, witch took hits == players dealt that much hits
        //Meanwhile player took hits is damage to score

        //Data
        var baseScore = 0;
        var l1pTook = ScoreCounter.Level1TotalPlayerTookHits;
        var l1wTook = ScoreCounter.Level1TotalWitchTookHits;
        var l2pTook = ScoreCounter.Level2TotalPlayerTookHits;
        var l2wTook = ScoreCounter.Level2TotalWitchTookHits;
        var l3pTook = ScoreCounter.Level3TotalPlayerTookHits;
        var l3wTook = ScoreCounter.Level3TotalWitchTookHits;
        var retries = ScoreCounter.TotalRetries;

        //Multipliers
        var l1pTookM = 70;
        var l1wTookM = 100;
        var l2pTookM = 25;
        var l2wTookM = 50;
        var l3pTookM = 12;
        var l3wTookM = 30;
        var retriesM = 500;

        //Calculating
        var Stage1Score = l1wTook * l1wTookM - l1pTook * l1pTookM;
        var Stage2Score = l2wTook * l2wTookM - l2pTook * l2pTookM;
        var Stage3Score = l3wTook * l3wTookM - l3pTook * l3pTookM;
        var PenaltyScore = retries * retries * retriesM; //retries increase in quadratic fashion
        var TotalScore = baseScore - PenaltyScore
            + Stage1Score
            + Stage2Score
            + Stage3Score;

        return
            $"\nHighscore" +
            $"\n" +
            $"\n  Base score" +
            $"\n      {baseScore}" +
            $"\n  Stage 1" +
            $"\n      Dealt hits: {l1wTook} * {l1wTookM}" +
            $"\n      Recieved: {l1pTook} * {l1pTookM}" +
            $"\n      Subtotal: {Stage1Score}" +
            $"\n  Stage 2" +
            $"\n      Dealt hits: {l2wTook} * {l2wTookM}" +
            $"\n      Recieved: {l2pTook} * {l2pTookM}" +
            $"\n      Subtotal: {Stage2Score}" +
            $"\n  Stage 3" +
            $"\n      Dealt hits: {l3wTook} * {l3wTookM}" +
            $"\n      Recieved: {l3pTook} * {l3pTookM}" +
            $"\n      Subtotal: {Stage3Score}" +
            $"\n  Retries" +
            $"\n      {retries} * {retries} * {retriesM}" +
            $"\n  Total score" +
            $"\n      {TotalScore}" +
            $"\n" +
            $"\n" +
            $"\nThank you for playing!" +
            $"\n";
    }
}
