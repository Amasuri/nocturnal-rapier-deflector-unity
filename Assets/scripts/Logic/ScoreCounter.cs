using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public enum EntityType
    {
        Witch,
        Player
    }
    public EntityType entityType;
    public int Hits { get; private set; }
    static public int WitchTookHits = 0;
    static public int RapierTookHits = 0;

    static public int Level1TotalWitchTookHits = 0;
    static public int Level1TotalPlayerTookHits = 0;
    static public int Level2TotalWitchTookHits = 0;
    static public int Level2TotalPlayerTookHits = 0;
    static public int Level3TotalWitchTookHits = 0;
    static public int Level3TotalPlayerTookHits = 0;
    static public int TotalRetries = 0;

    public AudioSource unsheathe;
    private bool unsheathePlayed;
    public Sprite TookHit;
    public Sprite Normal;

    private SpriteRenderer render;

    /// <summary>
    /// Decorative function, time since last hit in milliseconds.
    /// </summary>
    private float HitStunMsLeft = 0f;
    private const float MaxHitstun = 500f;
    public bool IsHitStun => HitStunMsLeft > 0f;

    // Start is called before the first frame update
    private void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();

        if(entityType == EntityType.Player)
        {
            unsheathe = GetComponents<AudioSource>()[0];
            unsheathePlayed = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!unsheathePlayed && !CutInOverlayController.CutInEnabled)
        {
            unsheathe.Play();
            unsheathePlayed = true;
        }

        if (HitStunMsLeft > 0)
        {
            HitStunMsLeft -= Time.deltaTime * 1000f;
            if (HitStunMsLeft <= 0)
                render.sprite = Normal;
        }

        if (entityType == EntityType.Player)
            RapierTookHits = Hits;
        else if (entityType == EntityType.Witch)
            WitchTookHits = Hits;
    }

    internal static void IncreaseRetries()
    {
        TotalRetries++;
    }

    internal static void RememberCurrentScores()
    {
        if(BattleControl.CurrentBattleType == BattleControl.BattleType.First)
        {
            Level1TotalPlayerTookHits = RapierTookHits;
            Level1TotalWitchTookHits = WitchTookHits;
        }
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.Second)
        {
            Level2TotalPlayerTookHits = RapierTookHits;
            Level2TotalWitchTookHits = WitchTookHits;
        }
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.ThirdBoss)
        {
            Level3TotalPlayerTookHits = RapierTookHits;
            Level3TotalWitchTookHits = WitchTookHits;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile proj = collision.gameObject.GetComponent<Projectile>();
        if (proj != null)
            if(!proj.AlreadyHitSomething && proj.StartTimerAlreadyPassed)
            {
                Hits++;
                proj.DoHit();
                HitStunMsLeft = MaxHitstun;
                render.sprite = TookHit;
            }
    }

    /// <summary>
    /// The logic as follows: player must score at least a third of the points witch has. < 1 is player loosing, >= 1 is player winning.
    /// </summary>
    static public float GetScoreRatio()
    {
        float ret = 3 / ((float)RapierTookHits / (float)WitchTookHits);
        if (float.IsNaN(ret))
            return 0f;
        else if (float.IsInfinity(ret))
            return 99f;
        else
            return ret;
    }

    /// <summary>
    /// The logic as follows: player must score at least a third of the points witch has. < 1 is player loosing, >= 1 is player winning.
    /// </summary>
    static public bool GetIsPlayerWinning()
    {
        return GetScoreRatio() >= 1f ? true : false;
    }
}
