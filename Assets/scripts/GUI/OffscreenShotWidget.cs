using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenShotWidget : MonoBehaviour
{
    private const float arrowYpos = 0.85f;
    public Transform arrowPrefab;

    private Dictionary<Transform, Transform> offscreenProj;

    // Start is called before the first frame update
    private void Start()
    {
        offscreenProj = new Dictionary<Transform, Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Poll all current projectiles and add those vertically higher than upper screen edge
        //Add newly polled ones to batch of projectiles above
        foreach (var p in PatternShooter.shotList)
        {
            var screenPos = Camera.main.WorldToScreenPoint(p.position);
            if (screenPos.y > Screen.height && !offscreenProj.ContainsKey(p))
            {
                var arrow = Instantiate(arrowPrefab, new Vector3(p.position.x, arrowYpos, p.position.z), new Quaternion(), gameObject.transform);
                offscreenProj.Add(p, arrow);
            }
        }

        //Sync arrow positions with their projectiles
        foreach (var p in offscreenProj.Keys)
        {
            offscreenProj[p].position = new Vector3(p.position.x, arrowYpos, p.position.z);
        }

        //Poll for removal: either if key/val gets destroyed or if key pos.Y no longer above screen
        var pendingRemoval = new List<Transform>();
        foreach (var p in offscreenProj.Keys)
        {
            var screenPos = Camera.main.WorldToScreenPoint(p.position);
            if (screenPos.y < Screen.height)
                pendingRemoval.Add(p);
        }
        foreach (var key in pendingRemoval)
        {
            Destroy(offscreenProj[key].gameObject);
            offscreenProj.Remove(key);
        }

        ////Spawning shot instance on field w/ tying it to parent for scale
        //var shotTransform = Instantiate(shotPrefab, transform.position, new Quaternion(), gameObject.transform) as Transform;
        //shotTransform.GetComponent<Projectile>().SetVelocityByType(projType, smallRandomize: explosionRandomize, strongRandomize: shotgun);
    }
}
