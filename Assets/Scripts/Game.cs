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

        public static int ShotsFired;
        public static int ShotsMax;
        public static float Timer;
        public static string LevelTitle;
        public static bool LevelCompleted = false;

        public static int ALevelShots;
        public static float ALevelTime;

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
    }
}
