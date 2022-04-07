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

    public AudioSource unsheathe;
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
            unsheathe.Play();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(HitStunMsLeft > 0)
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
