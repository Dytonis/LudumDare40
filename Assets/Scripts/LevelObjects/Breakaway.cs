using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD40.Events;
using UnityEngine;
using System.Collections;

namespace LD40.LevelObjects
{
    [RequireComponent(typeof(Events.EventHandler))]
    public class Breakaway : ManagedObject
    {
        public int Health;
        public float FadeTime;

        public override void OnReset()
        {
            StopAllCoroutines();

            t = 100;
            GetComponent<MeshRenderer>().enabled = true;
            Color c = GetComponent<MeshRenderer>().material.color;
            GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, 1);
            GetComponent<Collider>().enabled = true;
        }

        public override void OnManagedEvent(ManagedEventType e)
        {
            if (e == ManagedEventType.OnProjectileHit)
            {
                Health--;
                if (Health <= 0)
                {
                    Game.GetAudio().Break.Play();
                    StartCoroutine(Break(100f / FadeTime));
                    GetComponent<Collider>().enabled = false;

                    Game.ForceProjectilesNewTarget();
                }
            }
        }

        float t = 100;
        public IEnumerator Break(float Speed)
        {
            while(t > 30)
            {
                Color c = GetComponent<MeshRenderer>().material.color;
                t -= Time.deltaTime * Time.timeScale * Speed;
                GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, t / 100f);
                yield return new WaitForEndOfFrame();
            }

            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
