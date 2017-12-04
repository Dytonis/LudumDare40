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

        public bool OnlyTime;

        public int ALevelShotCount;
        public float ALevelSeconds;

        public Color BackgroundColor = Color.black;
        public bool END;

        public void Start()
        {
            Game.ShotsMax = maxShots;
            Game.GetShotCounter().UpdateShots();
            Game.LevelTitle = LevelTitle;
            Game.ALevelShots = ALevelShotCount;
            Game.ALevelTime = ALevelSeconds;
            Game.NextLevelTitle = NextLevelTile;
            Game.Projectiles.Clear();
            Game.GetFlasher().FlashWin();
            Game.IsOnlyTime = OnlyTime;
            Game.GetAudio().Flash.Play();
            Game.TargetsHit = 0;
            Camera.main.backgroundColor = BackgroundColor;
            Game.Demo = END;

            Game.GetController().StartReset();
        }

        public void Update()
        {
            Game.Timer += Time.deltaTime;
        }
    }
}
