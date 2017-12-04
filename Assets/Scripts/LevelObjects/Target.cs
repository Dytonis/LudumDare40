using LD40;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Target : ManagedObject
{
    public bool On;

    public override void OnReset()
    {
        base.OnReset();

        On = false;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void Start()
    {
        Game.Targets.Add(this);
    }

    public void Update()
    {
        if (On == false)
        {
            foreach (GameObject p in Game.Projectiles)
            {
                if (Vector3.Distance(transform.position, p.transform.position) < 2.5f)
                {
                    On = true;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;

                    Game.TargetsHit++;

                    if(Game.TargetsHit % 8 == 0)
                        Game.GetAudio().Dot1.Play();
                    if (Game.TargetsHit % 8 == 1)
                        Game.GetAudio().Dot2.Play();
                    if (Game.TargetsHit % 8 == 2)
                        Game.GetAudio().Dot3.Play();
                    if (Game.TargetsHit % 8 == 3)
                        Game.GetAudio().Dot4.Play();
                    if (Game.TargetsHit % 8 == 4)
                        Game.GetAudio().Dot5.Play();
                    if (Game.TargetsHit % 8 == 5)
                        Game.GetAudio().Dot6.Play();
                    if (Game.TargetsHit % 8 == 6)
                        Game.GetAudio().Dot7.Play();
                    if (Game.TargetsHit % 8 == 7)
                        Game.GetAudio().Dot8.Play();

                    if (Game.Targets.All(x => x.On == true))
                    {
                        Game.GetGate().LowerGate();
                        Game.GetAudio().DotWin.Play();
                    }
                }
            }
        }
    }
}
