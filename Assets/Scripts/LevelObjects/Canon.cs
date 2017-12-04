using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40.LevelObjects
{
    public class Canon : ManagedObject
    {
        public bool On;
        public bool DefaultOn;
        public MeshRenderer Indicator;
        public Material OnMaterial;
        public Material OffMaterial;
        public int MaxShots;

        public float Cooldown;

        public Projectile Prefab;

        private int shots;

        public Transform Muzzle;

        private float cd;

        public override void OnReset()
        {
            shots = 0;
            cd = Cooldown;
            On = DefaultOn;
        }

        public void Activate()
        {
            On = true;
            Indicator.material = OnMaterial;
        }

        public void Deactivate()
        {
            On = false;
            Indicator.material = OffMaterial;
        }

        public void Start()
        {
            cd = Cooldown;
        }

        public void Update()
        {
            if(cd > 0)
                cd -= Time.deltaTime * Time.timeScale;

            if(cd <= 0 && On && (shots + 1 <= MaxShots || MaxShots < 0))
            {
                shots++;
                cd = Cooldown;

                Projectile p = Instantiate(Prefab) as Projectile;

                p.Tank = Game.GetController();
                p.Color = Color.red;
                p.Player = false;
                p.transform.position = Muzzle.position;
                p.Launch(transform.forward, Muzzle.position);
                Game.GetAudio().Fire.Play();
                ThrowEvent(Events.ManagedEventType.OnShoot);
                if (shots == 1)
                    ThrowEvent(Events.ManagedEventType.OnFirstShot);
            }
        }
    }
}
