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

    public AudioSource unsheathe;

    // Start is called before the first frame update
    private void Start()
    {
        if(entityType == EntityType.Player)
        {
            unsheathe = GetComponents<AudioSource>()[0];
            unsheathe.Play();
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile proj = collision.gameObject.GetComponent<Projectile>();
        if (proj != null)
            if(!proj.HitOccured)
            {
                Hits++;
                proj.DoHit();
            }
    }
}
