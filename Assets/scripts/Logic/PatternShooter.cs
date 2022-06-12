using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatternShooter : MonoBehaviour
{
    public static List<Transform> shotList = new List<Transform>();

    public Transform shotPrefabSmall;
    public Transform shotPrefabNormal;
    public Transform shotPrefabBig;

    public AudioSource explosionSound;

    public float shootingRateLightningFast = 0.06f;
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
        DirectSlow,
        DirectFast,
        HalfArcSlow,
        ArcSlow,

        AlternatingArcDirectSlow,
        AlternatingArcDirectFast,

        WaitFirst,
        WaitSecond,
        WaitThird,

        ShotgunSlow,
        ShotgunFast,
        LaserRay,
        MachineGunFirst,
        MachineGunSecond,

        ExplosionSmallFirst,
        ExplosionSmallSecond,
        ExplosionMedium,
        ExplosionLarge,
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
            currentSmallShootingPattern = SmallShootingPattern.DirectSlow;
        }
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.Second)
        {
            currentBattleShootingPattern = BattleShootingPattern.SecondBattleMedium;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionSmallFirst;
        }
        else if (BattleControl.CurrentBattleType == BattleControl.BattleType.ThirdBoss)
        {
            currentBattleShootingPattern = BattleShootingPattern.ThirdBattleHard;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionLarge;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (CutInOverlayController.CutInEnabled)
            return;

        shootCooldown -= Time.deltaTime;
        if(shootCooldown <= 0f)
        {
            if (currentBattleShootingPattern == BattleShootingPattern.FirstBattleEasy)
                ActFirstBattlePattern();
            else if (currentBattleShootingPattern == BattleShootingPattern.SecondBattleMedium)
                ActSecondBattlePattern();
            else if (currentBattleShootingPattern == BattleShootingPattern.ThirdBattleHard)
                ActThirdBattlePattern();
        }

        PollCurrentShots();
    }

    private void ActThirdBattlePattern()
    {
        AttackByCurrentSmallPattern();

        if (currentSmallShootingPattern == SmallShootingPattern.ExplosionLarge && shotsOnCurrentSmallPattern >= 7)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.WaitFirst;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.WaitFirst && shotsOnCurrentSmallPattern >= 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.MachineGunFirst;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.MachineGunFirst && shotsOnCurrentSmallPattern >= 10)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionSmallFirst;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionSmallFirst && shotsOnCurrentSmallPattern >= 3 * 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.MachineGunSecond;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.MachineGunSecond && shotsOnCurrentSmallPattern >= 10)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionSmallSecond;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionSmallSecond && shotsOnCurrentSmallPattern >= 3 * 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.WaitSecond;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.WaitSecond && shotsOnCurrentSmallPattern >= 1)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.AlternatingArcDirectSlow;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.AlternatingArcDirectSlow && shotsOnCurrentSmallPattern >= 6)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.AlternatingArcDirectFast;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.AlternatingArcDirectFast && shotsOnCurrentSmallPattern >= 10)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ShotgunSlow;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ShotgunSlow && shotsOnCurrentSmallPattern >= 3 * 4)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ShotgunFast;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ShotgunFast && shotsOnCurrentSmallPattern >= 5 * 4)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.WaitThird;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.WaitThird && shotsOnCurrentSmallPattern >= 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.LaserRay;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.LaserRay && shotsOnCurrentSmallPattern >= 30)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionLarge;
        }
    }

    private void ActSecondBattlePattern()
    {
        AttackByCurrentSmallPattern();

        if (currentSmallShootingPattern == SmallShootingPattern.ExplosionSmallFirst && shotsOnCurrentSmallPattern >= 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.DirectFast;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.DirectFast && shotsOnCurrentSmallPattern >= 2)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.WaitFirst;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.WaitFirst && shotsOnCurrentSmallPattern >= 1)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.HalfArcSlow;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.HalfArcSlow && shotsOnCurrentSmallPattern >= 4)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionMedium;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionMedium && shotsOnCurrentSmallPattern >= 5)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.WaitSecond;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.WaitSecond && shotsOnCurrentSmallPattern >= 1)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.MachineGunFirst;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.MachineGunFirst && shotsOnCurrentSmallPattern >= 20)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionSmallFirst;
        }
    }

    private void ActFirstBattlePattern()
    {
        AttackByCurrentSmallPattern();

        if (currentSmallShootingPattern == SmallShootingPattern.DirectSlow && shotsOnCurrentSmallPattern >= 5)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.HalfArcSlow;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.HalfArcSlow && shotsOnCurrentSmallPattern >= 4)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ArcSlow;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ArcSlow && shotsOnCurrentSmallPattern >= 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.WaitFirst;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.WaitFirst && shotsOnCurrentSmallPattern >= 1)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.ExplosionSmallFirst;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionSmallFirst && shotsOnCurrentSmallPattern >= 3)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.WaitSecond;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.WaitSecond && shotsOnCurrentSmallPattern >= 2)
        {
            shotsOnCurrentSmallPattern = 0;
            currentSmallShootingPattern = SmallShootingPattern.DirectSlow;
        }
    }

    private void AttackByCurrentSmallPattern()
    {
        if (currentSmallShootingPattern == SmallShootingPattern.DirectSlow)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Line;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.DirectFast)
        {
            shootCooldown = shootingRateFast;
            var projType = Projectile.Type.Line;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.HalfArcSlow)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Curved;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ArcSlow)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Mortar;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.WaitFirst || currentSmallShootingPattern == SmallShootingPattern.WaitSecond || currentSmallShootingPattern == SmallShootingPattern.WaitThird)
        {
            shootCooldown = shootingRateSlow;

            //No projectiles, simply wait
            shotsOnCurrentSmallPattern++;
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionSmallFirst || currentSmallShootingPattern == SmallShootingPattern.ExplosionSmallSecond)
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

            for (int i = 0; i < 5; i++)
                FireSingleShot(projType, explosionRandomize: true);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ExplosionLarge)
        {
            shootCooldown = shootingRateSlow;
            var projType = Projectile.Type.Mortar;

            explosionSound.Play();

            for (int i = 0; i < 7; i++)
                FireSingleShot(projType, explosionRandomize: true);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.MachineGunFirst || currentSmallShootingPattern == SmallShootingPattern.MachineGunSecond)
        {
            shootCooldown = shootingRateVeryFast;
            var projType = Projectile.Type.FastLine;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.LaserRay)
        {
            shootCooldown = shootingRateLightningFast;
            var projType = Projectile.Type.FastLine;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.AlternatingArcDirectSlow)
        {
            shootCooldown = shootingRateSlow;

            var projType = Projectile.Type.Line;
            if (shotsOnCurrentSmallPattern % 2 == 0)
                projType = Projectile.Type.Mortar;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.AlternatingArcDirectFast)
        {
            shootCooldown = shootingRateFast;

            var projType = Projectile.Type.Line;
            if (shotsOnCurrentSmallPattern % 2 == 0)
                projType = Projectile.Type.Mortar;

            FireSingleShot(projType);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ShotgunSlow)
        {
            shootCooldown = shootingRateSlow;

            var projType = Projectile.Type.Curved;

            for (int i = 0; i < 3; i++)
                FireSingleShot(projType, shotgun: true);
        }
        else if (currentSmallShootingPattern == SmallShootingPattern.ShotgunFast)
        {
            shootCooldown = shootingRateFast;

            var projType = Projectile.Type.Curved;

            for (int i = 0; i < 3; i++)
                FireSingleShot(projType, shotgun: true);
        }
    }

    private void FireSingleShot(Projectile.Type projType, bool explosionRandomize = false, bool shotgun = false)
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
        shotTransform.GetComponent<Projectile>().SetVelocityByType(projType, smallRandomize: explosionRandomize, strongRandomize: shotgun);

        //Update counter for patterns
        shotsOnCurrentSmallPattern++;
    }

    private void PollCurrentShots()
    {
        var projList = new List<Transform>();
        var transf = gameObject.transform;

        for (int i = 0; i < transf.childCount; i++)
        {
            var ch = transf.GetChild(i);
            if (ch.GetComponent<Projectile>() != null)
                projList.Add(ch);
        }

        shotList = projList;
    }
}
