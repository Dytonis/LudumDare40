using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LD40.UI
{
    public class ShotCounter : MonoBehaviour
    {
        public Text[] Texts;

        public void UpdateShots()
        {
            if (!Game.IsOnlyTime)
            {
                foreach (Text t in Texts)
                {
                    t.text = "Shells fired: " + Game.ShotsFired + "/" + Game.ShotsMax;
                }
            }
            else
            {
                foreach (Text t in Texts)
                {
                    t.text = "No shot score!";
                }
            }
        }
    }
}
