using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40.Levels
{
    public class Level : MonoBehaviour
    {
        public int maxShots;
        public string LevelTitle;
        public string NextLevelTile;

        public int ALevelShotCount;
        public float ALevelSeconds;

        public Color BackgroundColor = Color.black;

        public void Start()
        {
            Game.ShotsMax = maxShots;
            Game.GetShotCounter().UpdateShots();
            Game.LevelTitle = LevelTitle;
            Game.ALevelShots = ALevelShotCount;
            Game.ALevelTime = ALevelSeconds;
            Game.NextLevelTitle = NextLevelTile;
            Game.Projectiles.Clear();
            Game.GetController().flasher.FlashWin();

            Camera.main.backgroundColor = BackgroundColor;

            Game.GetController().StartReset();
        }

        public void Update()
        {
            Game.Timer += Time.deltaTime;
        }
    }
}
