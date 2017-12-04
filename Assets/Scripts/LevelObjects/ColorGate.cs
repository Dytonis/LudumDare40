using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40.LevelObjects
{
    public class ColorGate : ManagedObject
    {
        public Color Color;

        public void Start()
        {
            GetComponent<MeshRenderer>().materials[1].color = new Color(Color.r, Color.g, Color.b, 0.5f);
        }

        public void StopIf(Projectile p)
        {
            Color c1 = new Color(p.GetComponent<MeshRenderer>().material.color.r, p.GetComponent<MeshRenderer>().material.color.g, p.GetComponent<MeshRenderer>().material.color.b, 1);
            Color c2 = new Color(Color.r, Color.g, Color.b, 1);

            if (c1 != c2)
            {   
                p.Stop();
            }
        }
    }
}
