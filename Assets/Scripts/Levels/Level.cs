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

        public int ALevelShotCount;
        public float ALevelSeconds;

        public void Start()
        {
            Game.ShotsMax = maxShots;
            Game.GetShotCounter().UpdateShots();
            Game.LevelTitle = LevelTitle;
            Game.ALevelShots = ALevelShotCount;
            Game.ALevelTime = ALevelSeconds;
        }

        public void Update()
        {
            Game.Timer += Time.deltaTime;
        }
    }
}
