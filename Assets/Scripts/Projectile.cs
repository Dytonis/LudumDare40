using LD40;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ManagedObject
{
    public float Speed;
    public Controller Tank;
    public Vector3 targetDir;
    public Vector3 lastTarget;
    public Vector3 target;

    public bool fastStart = false;

    private bool death = false;

    public void Start()
    {
        if(fastStart)
            Launch(transform.forward, transform.position);
    }

    public void Launch(Vector3 dir, Vector3 start)
    {
        transform.eulerAngles = new Vector3(90, 0, 0);

        lastTarget = start;
        targetDir = getNewTarget(dir, start, out target);
        Debug.DrawLine(target, transform.position, Color.green, 1f);
    }

    public void Update()
    {
        if (Game.FreezeTrails)
            GetComponent<TrailRenderer>().time = 10000;
        else
            GetComponent<TrailRenderer>().time = 1;

        if (target != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * Speed * Time.timeScale);

            Debug.DrawRay(transform.position, targetDir, Color.blue, Time.fixedDeltaTime);

            if(Vector3.Distance(transform.position, target) < 0.1f)
            {
                targetDir = getNewTarget(targetDir, target, out target);
                Debug.DrawLine(target, transform.position, Color.green, 1f);
            }

            if (Tank)
            {
                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Tank.transform.position.x, 0, Tank.transform.position.z)) < 2)
                {
                    if (!Tank.Exploded && !Tank.Godmode)
                    {
                        Tank.Exploded = true;
                        Tank.StartOnDeath();
                        Time.timeScale = 0.25f;
                        Game.FreezeTrails = true;
                        Tank.flasher.Flash();
                        Tank.top.gameObject.SetActive(false);
                        Tank.bottom.gameObject.SetActive(false);
                        Tank.exploded.gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            transform.position = (transform.position + (targetDir * Time.deltaTime * Speed * Time.timeScale));
            if (death == false)
                Destroy(gameObject, 2);
            death = true;
        }
    }

    private Vector3 getNewTarget(Vector3 dir, Vector3 start, out Vector3 target)
    {
        RaycastHit h;
        Vector3 bounceDir = dir;

        if (Physics.Raycast(ray: new Ray(start, dir), hitInfo: out h, layerMask: 1 << LayerMask.NameToLayer("wall"), maxDistance: 1000000))
        {
            bounceDir = newDir(dir, h.normal);
            target = h.point;
        }
        else
            target = Vector3.zero;

        Debug.DrawRay(start, bounceDir, Color.yellow, 1f);

        return bounceDir;
    }

    private Vector3 newDir(Vector3 velIn, Vector3 normal)
    {
        Vector3 newAngle = (Quaternion.AngleAxis(180, normal) * -velIn.normalized).normalized;
        return new Vector3(newAngle.x, 0, newAngle.z).normalized;
    }

    public override void Reset()
    {
        Game.Projectiles.Remove(gameObject);
        Destroy(gameObject);
    }
}
