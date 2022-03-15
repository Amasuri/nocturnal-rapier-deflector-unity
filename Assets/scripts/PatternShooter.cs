using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatternShooter : MonoBehaviour
{
    public Transform shotPrefabBig;
    public float shootingRateFast = 0.25f;
    public float shootingRateAverage = 0.5f;
    public float shootingRateSlow = 1f;
    private float shootCooldown;

    public ShootingPattern currentShootingPattern;
    public enum ShootingPattern
    {
        Random,
        MachineGun,
        Differentiating,
    }

    // Start is called before the first frame update
    private void Start()
    {
        shootCooldown = 0f;
        currentShootingPattern = ShootingPattern.MachineGun;
    }

    // Update is called once per frame
    private void Update()
    {
        shootCooldown -= Time.deltaTime;
        if(shootCooldown <= 0f)
        {
            AttackByCurrentPattern();
        }
    }

    private void AttackByCurrentPattern()
    {
        if (currentShootingPattern == ShootingPattern.Random)
        {
            shootCooldown = shootingRateAverage;

            var projType = (Projectile.Type)Random.Range(0, (int)Projectile.Type.FastLine);
            FireSingleShot(projType);
        }
        else if (currentShootingPattern == ShootingPattern.MachineGun)
        {
            shootCooldown = shootingRateFast;
            var projType = Projectile.Type.FastLine;

            FireSingleShot(projType);
        }
    }

    private void FireSingleShot(Projectile.Type projType)
    {
        var shotTransform = Instantiate(shotPrefabBig, transform.position, new Quaternion(), gameObject.transform) as Transform;
        shotTransform.GetComponent<Projectile>().SetVelocityByType(projType);
    }
}
