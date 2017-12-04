using LD40;
using LD40.Events;
using LD40.LevelObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ManagedObject
{
    private Color c;
    public Color Color
    {
        get
        {
            return c;
        }
        set
        {
            c = value;
            GetComponent<MeshRenderer>().material.color = value;
        }
    }
    public float Speed;
    public Controller Tank;
    public bool Player = true;
    public Vector3 nextTargetDir;
    public Vector3 lastTarget;
    public Vector3 target;
    public Vector3 gateTarget;
    public ColorGate gateObject;
    public GameObject targetObject;

    public Vector3 PosLast;
    public Vector3 CurrentDir;

    public bool fastStart = false;

    [HideInInspector]
    public bool go = true;

    public bool CanKill = false;

    private bool death = false;

    public void Start()
    {
        if (fastStart)
            Launch(transform.forward, transform.position);
    }

    public void Launch(Vector3 dir, Vector3 start)
    {
        lastTarget = start;
        nextTargetDir = getNewTarget(dir, start, out target, out targetObject);
        Debug.DrawLine(target, transform.position, Color.green, 1f);
        Game.GetAudio().Fire.Play();
    }

    public void UpdateTargetInDirection()
    {
        Debug.DrawLine(transform.position, target, Color.white, 1f);
        updateNewTarget((target - transform.position).normalized, transform.position, out target, out targetObject);
        //Debug.DrawLine(transform.position, target, Color.blue, 1f);
    }

    public void Update()
    {
        if (!go)
            return;

        if (Game.FreezeTrails)
            GetComponent<TrailRenderer>().time = 10000;
        else
            GetComponent<TrailRenderer>().time = 1;
        Debug.DrawRay(target, Vector3.up * 30, Color.magenta, Time.deltaTime);
        if (target != Vector3.zero)
        {

            PosLast = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * Speed * Time.timeScale);

            CurrentDir = (transform.position - PosLast);

            Debug.DrawRay(transform.position, nextTargetDir, Color.blue, Time.fixedDeltaTime);

            if (Vector3.Dot(CurrentDir.normalized, (gateTarget - transform.position).normalized) < -0.8f)
            {
                if (gateTarget != Vector3.zero)
                {
                    gateObject.StopIf(this);
                }
            }

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                CanKill = true;

                if (targetObject.tag == "killprojectile")
                {
                    Game.Projectiles.Remove(this.gameObject);
                    Destroy(this.gameObject);
                }

                if (targetObject.GetComponent<EventHandler>() && Player)
                {
                    EventHandler h = targetObject.GetComponent<EventHandler>();
                    if (h.TargetEvent == ManagedEventType.OnProjectileHit)
                    {
                        h.EventThrow(this, ManagedEventType.OnProjectileHit);
                    }
                }
                else if (targetObject.GetComponent<EventHandler>())
                {
                    EventHandler h = targetObject.GetComponent<EventHandler>();
                    if (h.TargetEvent == ManagedEventType.OnDroneProjectileHit)
                    {
                        h.EventThrow(this, ManagedEventType.OnDroneProjectileHit);
                    }
                }

                nextTargetDir = getNewTarget(nextTargetDir, target, out target, out targetObject);
                if (Time.timeScale > 0.5f)
                    Game.GetAudio().Bounce3.Play();
                else
                    Game.GetAudio().Bounce1.Play();
                Debug.DrawLine(target, transform.position, Color.green, 1f);
            }

            if (Tank)
            {
                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Tank.transform.position.x, 0, Tank.transform.position.z)) < 2)
                {
                    if (!Tank.Exploded && !Tank.Godmode && CanKill)
                    {
                        Tank.Exploded = true;
                        Tank.StartOnDeath();
                        Time.timeScale = 0.25f;
                        Game.FreezeTrails = true;
                        Game.GetFlasher().Flash();
                        Tank.top.gameObject.SetActive(false);
                        Tank.bottom.gameObject.SetActive(false);
                        Tank.exploded.gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            transform.position = (transform.position + (nextTargetDir * Time.deltaTime * Speed * Time.timeScale));
            if (death == false)
            {
                Destroy(gameObject, 2);
                Game.Projectiles.Remove(gameObject);
            }
            death = true;
        }

        transform.Rotate(new Vector3(0, 120 * Time.deltaTime * Time.timeScale, 0));
    }

    public void Stop()
    {
        Game.Projectiles.Remove(gameObject);
        Destroy(gameObject, GetComponent<TrailRenderer>().time);
        go = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Light>().enabled = false;
    }

    private Vector3 getNewTarget(Vector3 dir, Vector3 start, out Vector3 target, out GameObject targetObject)
    {
        RaycastHit h;
        RaycastHit hGate;
        Vector3 bounceDir = dir;

        Debug.DrawRay(start, dir * 1000, Color.yellow, 1f);

        if (Physics.Raycast(ray: new Ray(start, dir), hitInfo: out hGate, layerMask: 1 << LayerMask.NameToLayer("colorgate"), maxDistance: 1000000))
        {
            if (hGate.collider.tag == "colorgate")
            {
                gateObject = hGate.collider.GetComponent<ColorGate>();
                gateTarget = hGate.point;
            }
        }
        else
        {
            gateObject = null;
            gateTarget = Vector3.zero;
        }

        if (Physics.Raycast(ray: new Ray(start, dir), hitInfo: out h, layerMask: 1 << LayerMask.NameToLayer("wall"), maxDistance: 1000000))
        {
            bounceDir = newDir(dir, h.normal);
            target = h.point;
            targetObject = h.transform.gameObject;
        }
        else
        {
            target = Vector3.zero;
            targetObject = null;
        }

        //Debug.DrawRay(start, bounceDir, Color.yellow, 1f);

        return bounceDir;
    }

    private Vector3 updateNewTarget(Vector3 dir, Vector3 start, out Vector3 target, out GameObject targetObject)
    {
        RaycastHit h;
        Vector3 bounceDir = dir;

        Debug.DrawRay(start, dir * 1000, Color.red, 1f);

        if (Physics.Raycast(ray: new Ray(start, bounceDir), hitInfo: out h, layerMask: 1 << LayerMask.NameToLayer("wall"), maxDistance: 1000000))
        {
            target = h.point;
            targetObject = h.transform.gameObject;
        }
        else
        {
            target = Vector3.zero;
            targetObject = null;
        }

        //Debug.DrawRay(start, bounceDir * 1000, Color.yellow, 1f);

        return bounceDir;
    }

    private Vector3 newDir(Vector3 velIn, Vector3 normal)
    {
        Vector3 newAngle = (Quaternion.AngleAxis(180, normal) * -velIn.normalized).normalized;
        return new Vector3(newAngle.x, 0, newAngle.z).normalized;
    }

    public override void OnReset()
    {
        Game.Projectiles.Remove(gameObject);
        Destroy(gameObject);
    }
}
