using LD40;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Target : ResetableObject
{
    public bool On;

    public override void Reset()
    {
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
                if (Vector3.Distance(transform.position, p.transform.position) < 2f)
                {
                    On = true;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;

                    if (Game.Targets.All(x => x.On == true))
                    {
                        Game.GetGate().LowerGate();
                    }
                }
            }
        }
    }
}
