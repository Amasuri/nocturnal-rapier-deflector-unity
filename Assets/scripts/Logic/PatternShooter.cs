using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatternShooter : MonoBehaviour
{
    public Transform shotPrefabSmall;
    public Transform shotPrefabNormal;
    public Transform shotPrefabBig;

    public AudioSource explosionSound;

    public float shootingRateVeryFast = 0.125f;
    public float shootingRateFast = 0.25f;
    public float shootingRateAverage = 0.5f;
    public float shootingRateSlow = 1f;
    private float shootCooldown;
    private int shotsOnCurrentSmallPattern = 0;

    private SmallShootingPattern currentSmallShootingPattern;
    public BattleShootingPattern currentBattleShootingPattern;
    public enum SmallShootingPattern
    {
        SlowDirect,
        SlowHalfArc,
        SlowArc,
        ExplosionSmall,
        FirstWait,
        SecondWait,

        MachineGun,
        FastDirect,
        ExplosionMedium,
    }

    public enum BattleShootingPattern
    {
        FirstBattleEasy,
        SecondBattleMedium,
        ThirdBattleHard
    }

    // Start is called before the first frame update
    private void Start()
    {
        shootCooldown = 0f;
        explosionSound = GetComponents<AudioSource>()[0];

        if (BattleControl.CurrentBattleType == BattleControl.BattleType.First)
        {
            currentBattleShootingPattern = BattleShootingPattern.FirstBattleEasy;
            currentSmallShootingPattern = SmallShootingPattern.SlowDirect;
        }
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.Second)
        {
            currentBattleShootingPattern = BattleShootingPattern.SecondBattleMedium;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionSmall;
        }
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.ThirdBoss)
        {
            currentBattleShootingPattern = BattleShootingPattern.ThirdBattleHard;
            currentSmallShootingPattern = SmallShootingPattern.SlowDirect;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        shootCooldown -= Time.deltaTime;
        if(shootCooldown <= 0f)
        {
            if (currentBattleShootingPattern == BattleShootingPattern.FirstBattleEasy)
            {
                ActFirstBattlePattern();
            }
            else if (currentBattleShootingPattern == BattleShootingPattern.SecondBattleMedium)
            {
                ActSecondBattlePattern();
            }
            else if (currentBattleShootingPattern == BattleShootingPattern.ThirdBattleHard)
            {
                ActFirstBattlePattern();
            }
        }
    }

    private void ActSecondBattlePattern()
    {
        AttackByCurrentSmallPattern();

        if (currentSmallShootingPattern == SmallShootingPattern.ExplosionSmall && shotsOnCurrentSmallPattern >= 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.FastDirect;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.FastDirect && shotsOnCurrentSmallPattern >= 2)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.FirstWait;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.FirstWait && shotsOnCurrentSmallPattern >= 1)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.SlowHalfArc;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.SlowHalfArc && shotsOnCurrentSmallPattern >= 4)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionMedium;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionMedium && shotsOnCurrentSmallPattern >= 5)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.SecondWait;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.SecondWait && shotsOnCurrentSmallPattern >= 1)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.MachineGun;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.MachineGun && shotsOnCurrentSmallPattern >= 20)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionSmall;
        }
    }

    private void ActFirstBattlePattern()
    {
        AttackByCurrentSmallPattern();

        if (currentSmallShootingPattern == SmallShootingPattern.SlowDirect && shotsOnCurrentSmallPattern >= 5)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.SlowHalfArc;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.SlowHalfArc && shotsOnCurrentSmallPattern >= 4)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.SlowArc;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.SlowArc && shotsOnCurrentSmallPattern >= 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.FirstWait;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.FirstWait && shotsOnCurrentSmallPattern >= 1)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionSmall;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionSmall && shotsOnCurrentSmallPattern >= 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.SecondWait;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.SecondWait && shotsOnCurrentSmallPattern >= 2)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.SlowDirect;
        }
    }

    private void AttackByCurrentSmallPattern()
    {
        if (currentSmallShootingPattern == SmallShootingPattern.SlowDirect)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Line;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.FastDirect)
        {
            shootCooldown = shootingRateFast;
            var projType = Projectile.Type.Line;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.SlowHalfArc)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Curved;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.SlowArc)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Mortar;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.FirstWait || currentSmallShootingPattern == SmallShootingPattern.SecondWait)
        {
            shootCooldown = shootingRateSlow;

            //No projectiles, simply wait
            shotsOnCurrentSmallPattern++;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionSmall)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Mortar;

            explosionSound.Play();

            FireSingleShot(projType, explosionRandomize: true);
            FireSingleShot(projType, explosionRandomize: true);
            FireSingleShot(projType, explosionRandomize: true);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionMedium)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Mortar;

            explosionSound.Play();

            FireSingleShot(projType, explosionRandomize: true);
            FireSingleShot(projType, explosionRandomize: true);
            FireSingleShot(projType, explosionRandomize: true);
            FireSingleShot(projType, explosionRandomize: true);
            FireSingleShot(projType, explosionRandomize: true);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.MachineGun)
        {
            shootCooldown = shootingRateVeryFast;
            var projType = Projectile.Type.FastLine;

            FireSingleShot(projType);
        }
    }

    private void FireSingleShot(Projectile.Type projType, bool explosionRandomize = false)
    {
        //Selecting shot from three different types, 33% chance each
        var rand = Random.Range(0, 100);
        var shotPrefab = shotPrefabBig;
        if (rand <= 33)
            shotPrefab = shotPrefabNormal;
        else if (rand > 33 && rand <= 66)
            shotPrefab = shotPrefabSmall;

        //Spawning shot instance on field w/ tying it to parent for scale
        var shotTransform = Instantiate(shotPrefab, transform.position, new Quaternion(), gameObject.transform) as Transform;
        shotTransform.GetComponent<Projectile>().SetVelocityByType(projType, smallRandomize: explosionRandomize);

        //Update counter for patterns
        shotsOnCurrentSmallPattern++;
    }
}
