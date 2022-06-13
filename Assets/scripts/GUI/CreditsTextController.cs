using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CreditsTextController : MonoBehaviour
{
    public TextMeshProUGUI textTerminal;

    private float textStepSec;
    private const float textStepSecMax = 0.1f;
    private float originalY;

    private bool clickedToScore = false;

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

        if(pos.y <= 0.15f)
            transform.position = new Vector3(pos.x, pos.y + scroll, pos.z);

        if(Input.GetMouseButtonDown(0) && !clickedToScore)
        {
            textTerminal.text = GetScoreTextAndWriteToScoreFile();
            transform.position = new Vector3(pos.x, originalY, pos.z);
            clickedToScore = true;
        }
    }

    private string GetCreditsText()
    {
        if (TextPool.sceneLang == TextPool.SceneLanguage.English)
            return
            "\nCode & Concept:" +
            "\n   @AMasumari (Twitter)" +
            "\n     or amasuri.itch.io" +
            "\n" +
            "\nAwesome Music:" +
            "\n   @domainofeyes (Twitter)" +
            "\n     or sat9.bandcamp.com" +
            "\n" +
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
            "\n(Click for your score or check the score.txt if on PC)\n";
        else
            return
            "\n„K„€„t „y „x„p„t„…„}„{„p:" +
            "\n   @AMasumari (Twitter)," +
            "\n     „|„y„q„€ amasuri.itch.io" +
            "\n" +
            "\n„K„|„p„ƒ„ƒ„~„p„‘ „}„…„x„„{„p:" +
            "\n   @domainofeyes (Twitter)," +
            "\n     „|„y„q„€ sat9.bandcamp.com" +
            "\n" +
            "\n„K„|„p„ƒ„ƒ„~„„u „„y„{„ƒ„u„|„y:" +
            "\n   @YoukaiDrawing (Twitter)," +
            "\n     „|„y„q„€ vk.com/pixel_youkai" +
            "\n" +
            "\n„R„€„‰„~„„u „x„r„…„‰„{„y:" +
            "\n   „Q„p„x„~„„u „‚„u„q„‘„„„p „ƒ freesound.org:" +
            "\n   johnnyguitar01: single-firework" +
            "\n   tommccann: explosion-01" +
            "\n   johnbuhr: sword-clash-3" +
            "\n   qubodup: sword-hit" +
            "\n   misscellany: metallic-whoosh" +
            "\n   jamesabdulrahman: unsheathed-blade" +
            "\n   sypherzent: cut-through-armor-slice-clang" +
            "\n" +
            "\n„P„€„‚„„ „~„p „„~„y„„„y „y „‚„y„}„u„z„{ „y„s„‚„, „ƒ„t„u„|„p„~„~„€„z „r„€" +
            "\n„r„‚„u„}„‘ „‰„u„|„|„u„~„t„w„p \"3 „~„u„t„u„|„y 3 „y„s„‚„\" „~„p" +
            "\nMonogame, „€„‚„y„s„y„~„p„| „€„„ „„„€„z „w„u „{„€„}„p„~„t„." +
            "\n" +
            "\n„R„t„u„|„p„~„€ „ƒ „|„„q„€„r„Ž„ @AMasumari „y „t„‚„…„x„u„z <3" +
            "\n" +
            "\n„R„„p„ƒ„y„q„€ „x„p „y„s„‚„…!" +
            "\n(„N„p„w„}„y„„„u „t„|„‘ „„„€„s„€ „‰„„„€„q„ „…„x„~„p„„„Ž „ƒ„{„€„|„Ž„{„€ „r„" +
            "\n„~„p„q„‚„p„|„y „€„‰„{„€„r, „|„y„q„€ „„‚„€„r„u„‚„Ž„„„u score.txt," +
            "\n„u„ƒ„|„y „r„ „ƒ „{„€„}„„Ž„„„„u„‚„p)\n";
    }

    private string GetScoreTextAndWriteToScoreFile()
    {
        //Mobile workaround: initially planned to write scoreboards somewhere, but idk, writing permission makes the app kinda sus, right?
        var writeScore = true;
        if (SystemInfo.operatingSystem.Contains("Android"))
        {
            writeScore = false;
        }

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

        //Writing it down with a time stamp
        if (writeScore)
        {
            var fileS = "score.txt";
            if (!File.Exists(fileS))
            {
                var stream = File.Create(fileS);
                stream.Close();
            }
            File.AppendAllText(fileS, "\n" + DateTime.Now.ToString("g") + "\nScore: " + TotalScore.ToString() + "\n");
        }

        if (TextPool.sceneLang == TextPool.SceneLanguage.English)
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
                $"\nScreenshot this score and share with friends!" +
                $"\nYou can use #NRDscore if you want to." +
                $"\nThank you for playing <3" +
                $"\n";
        else
            return
            $"\n„Q„u„x„…„|„Ž„„„p„„" +
            $"\n" +
            $"\n  „A„p„x„€„r„„u „€„‰„{„y" +
            $"\n      {baseScore}" +
            $"\n  „T„‚„€„r„u„~„Ž 1" +
            $"\n      „N„p„~„u„ƒ„u„~„€ „„€ „s„€„|„€„r„u „‚„p„x: {l1wTook} * {l1wTookM}" +
            $"\n      „P„€„|„…„‰„u„~„€ „„€ „s„€„|„€„r„u „‚„p„x: {l1pTook} * {l1pTookM}" +
            $"\n      „H„p „…„‚„€„r„u„~„Ž: {Stage1Score}" +
            $"\n  „T„‚„€„r„u„~„Ž 2" +
            $"\n      „N„p„~„u„ƒ„u„~„€ „„€ „s„€„|„€„r„u „‚„p„x: {l2wTook} * {l2wTookM}" +
            $"\n      „P„€„|„…„‰„u„~„€ „„€ „s„€„|„€„r„u „‚„p„x: {l2pTook} * {l2pTookM}" +
            $"\n      „H„p „…„‚„€„r„u„~„Ž: {Stage2Score}" +
            $"\n  „T„‚„€„r„u„~„Ž 3" +
            $"\n      „N„p„~„u„ƒ„u„~„€ „„€ „s„€„|„€„r„u „‚„p„x: {l3wTook} * {l3wTookM}" +
            $"\n      „P„€„|„…„‰„u„~„€ „„€ „s„€„|„€„r„u „‚„p„x: {l3pTook} * {l3pTookM}" +
            $"\n      „H„p „…„‚„€„r„u„~„Ž: {Stage3Score}" +
            $"\n  „K„€„|„y„‰„u„ƒ„„„r„€ „„€„r„„„€„‚„u„~„y„z „…„‚„€„r„~„u„z" +
            $"\n      {retries} * {retries} * {retriesM}" +
            $"\n  „I„„„€„s„€ „€„‰„{„€„r:" +
            $"\n      {TotalScore}" +
            $"\n" +
            $"\n„R„t„u„|„p„z„„„u „ƒ„~„y„}„€„{ „„{„‚„p„~„p „ƒ„€ „ƒ„r„€„y„}„y „€„‰„{„p„}„y „y „„€„t„u„|„y„„„u„ƒ„Ž „ƒ „t„‚„…„x„Ž„‘„}„y!" +
            $"\n„M„€„w„u„„„u „y„ƒ„„€„|„Ž„x„€„r„p„„„Ž „„„u„s #NRDscore." +
            $"\n„R„„p„ƒ„y„q„€ „x„p „y„s„‚„… <3" +
            $"\n";
    }
}
