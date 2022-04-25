using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatternShooter : MonoBehaviour
{
    public Transform shotPrefabSmall;
    public Transform shotPrefabNormal;
    public Transform shotPrefabBig;

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
        Explosion,
        SmallWait,
        MediumWait,

        MachineGun,
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

        if (BattleControl.CurrentBattleType == BattleControl.BattleType.First)
        {
            currentBattleShootingPattern = BattleShootingPattern.FirstBattleEasy;
            currentSmallShootingPattern = SmallShootingPattern.SlowDirect;
        }
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.Second)
        {
            currentBattleShootingPattern = BattleShootingPattern.SecondBattleMedium;
            currentSmallShootingPattern = SmallShootingPattern.SlowDirect;
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
                AttackByCurrentSmallPattern();

                if(currentSmallShootingPattern == SmallShootingPattern.SlowDirect && shotsOnCurrentSmallPattern >= 5)
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
                    currentSmallShootingPattern = SmallShootingPattern.SlowDirect;
                }
            }
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
        else if (currentSmallShootingPattern == SmallShootingPattern.MachineGun)
        {
            shootCooldown = shootingRateFast;
            var projType = Projectile.Type.FastLine;

            FireSingleShot(projType);
        }
    }

    private void FireSingleShot(Projectile.Type projType)
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
        shotTransform.GetComponent<Projectile>().SetVelocityByType(projType);

        //Update counter for patterns
        shotsOnCurrentSmallPattern++;
    }
}
