using LD40.UI;
using LD40.UI.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40
{
    public class Game
    {
        public static bool FreezeTrails = false;
        public static List<GameObject> Projectiles = new List<GameObject>();
        public static List<Target> Targets = new List<Target>();

        public static bool Demo;

        public static int TargetsHit;
        public static int ShotsFired;
        public static int ShotsMax;
        public static float Timer;
        public static bool IsOnlyTime;
        public static string LevelTitle;
        public static bool LevelCompleted = false;
        public static string NextLevelTitle;

        public static int ALevelShots;
        public static float ALevelTime;

        public static void ForceProjectilesNewTarget()
        {
            foreach(GameObject o in Projectiles)
            {
                if(Vector3.Distance(o.GetComponent<Projectile>().target, o.transform.position) > 0.2f)
                o.GetComponent<Projectile>().UpdateTargetInDirection();
            }
        }

        public static Transform GetSpawn()
        {
            return GameObject.FindGameObjectWithTag("spawn").transform;
        }

        public static ShotCounter GetShotCounter()
        {
            return GameObject.FindGameObjectWithTag("shotcounter").GetComponent<ShotCounter>();
        }

        public static Gate GetGate()
        {
            return GameObject.FindGameObjectWithTag("gate").GetComponent<Gate>();
        }

        public static Canvas GetCanvas()
        {
            return GameObject.FindGameObjectWithTag("canvas").GetComponent<Canvas>();
        }

        public static MenuManager GetMenuManager()
        {
            return GameObject.FindGameObjectWithTag("canvas").GetComponent<MenuManager>();
        }

        public static Controller GetController()
        {
            return GameObject.FindGameObjectWithTag("Tank").GetComponent<Controller>();
        }

        public static MiddleTextHandler GetMiddleText()
        {
            return GameObject.FindGameObjectWithTag("middle").GetComponent<MiddleTextHandler>();
        }

        public static AudioManager GetAudio()
        {
            return GameObject.FindGameObjectWithTag("camerafollow").GetComponent<AudioManager>();
        }

        public static Flasher GetFlasher()
        {
            return GameObject.FindGameObjectWithTag("flasher").GetComponent<Flasher>();
        }
    }
}
