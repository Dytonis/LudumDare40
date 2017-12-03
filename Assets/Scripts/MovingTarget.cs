using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40
{
    public class MovingTarget : Target
    {
        public float Speed;
        public Transform Anchor1;
        public Transform Anchor2;

        private bool A1;

        public new void Update()
        {
            base.Update();

            if(A1)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, Anchor1.localPosition, Time.deltaTime * Time.timeScale * Speed);
                if(Vector3.Distance(transform.position, Anchor1.position) <= 0.1f)
                {
                    A1 = false;
                }
            }
            if (!A1)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, Anchor2.localPosition, Time.deltaTime * Time.timeScale * Speed);
                if (Vector3.Distance(transform.position, Anchor2.position) <= 0.1f)
                {
                    A1 = true;
                }
            }
        }
    }
}
