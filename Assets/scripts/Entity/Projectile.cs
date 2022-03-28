using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public AudioSource hit1;
    public AudioSource hit2;

    /// <summary>
    /// Default mouse velocity is very tiny to have any effect on the projectiles
    /// </summary>
    private const int MouseBouncinessModifier = 500;
    private const float NoStartCollideMaxSec = 0.1f;
    private float NoStartCollideTimer;

    public bool AlreadyHitSomething { get; private set; }
    public bool StartTimerAlreadyPassed => NoStartCollideTimer <= 0f;

    public Type type;
    public enum Type
    {
        Line = 0,
        Mortar = 1,
        Curved = 2,
        FastLine = 3,
    };

    public static List<Vector2> velocities = new List<Vector2>
    {
        new Vector2(2.3f, 0.3f), //hits
        new Vector2(0.5f, 2.65f), //hits
        new Vector2(1.15f, 1.15f), //hits
        new Vector2(2.8f, 0.3f), //hits
    };

    // Start is called before the first frame update
    private void Start()
    {
        //Auto-destroy after 20 seconds
        Destroy(gameObject, 20);
        NoStartCollideTimer = NoStartCollideMaxSec;

        AlreadyHitSomething = false;

        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);

        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        hit1 = GetComponents<AudioSource>()[0];
        hit2 = GetComponents<AudioSource>()[1];

        rb.velocity = velocities[(int)type];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Tiny window ensures no self-score when the witch spawns projectiles inside itself
        if (NoStartCollideTimer > 0f)
        {
            return;
        }

        //Rapier collision logic
        Rapier rap = collision.gameObject.GetComponent<Rapier>();
        if (rap != null)
        {
            Vector2 rapierVelocity = new Vector2(Rapier.RapierVelocity.x, Rapier.RapierVelocity.y);

            //Bounciness is already set via PhysicsMaterial2D, so what's left is apply mouse vector
            var resultingRapierVelocity = rapierVelocity * MouseBouncinessModifier;
            rb.AddForce(resultingRapierVelocity);

            //Debug.Log(resultingRapierVelocity);

            if (UnityEngine.Random.Range(0, 100) <= 50)
                hit1.Play();
            else
                hit2.Play();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Tiny window ensures no self-score when the witch spawns projectiles inside itself
        if (NoStartCollideTimer > 0f)
        {
            NoStartCollideTimer -= Time.deltaTime;
        }

        //this.gameObject.transform.Rotate(0, 0, 1f);
    }

    public void DoHit()
    {
        AlreadyHitSomething = true;
    }

    public void SetVelocityByType(Type vel)
    {
        this.type = vel;
        rb.velocity = velocities[(int)type];
    }
}
