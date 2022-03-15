using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatternShooter : MonoBehaviour
{
    public Transform shotPrefabBig;
    public float shootingRate = 0.5f;
    private float shootCooldown;
    private bool canAttack => shootCooldown <= 0f;

    public ShootingPattern shootingPattern;
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
    }

    // Update is called once per frame
    private void Update()
    {
        shootCooldown -= Time.deltaTime;
        if(shootCooldown <= 0f)
        {
            shootCooldown = shootingRate;

            var projType = (Projectile.Type)Random.Range(0, (int)Projectile.Type.FastLine);
            var shotTransform = Instantiate(shotPrefabBig, transform.position, new Quaternion(), gameObject.transform) as Transform;

            shotTransform.GetComponent<Projectile>().SetVelocityByType(projType);
        }
    }
}
