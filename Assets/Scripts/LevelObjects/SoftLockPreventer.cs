using LD40;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40.LevelObjects
{
    public class SoftLockPreventer : ManagedObject
    {
        public Door[] DoorChecks;

        public bool CheckAllTargets = true;
        public Target[] Targets;
        public float CheckFrequency;
        public bool KillIfTargetsExcist;

        public string Message;

        private float t;

        public void Start()
        {
            if (CheckAllTargets)
                Targets = Game.Targets.ToArray();
        }

        public override void OnReset()
        {
            if (CheckAllTargets)
                Targets = Game.Targets.ToArray();
        }

        public void Update()
        {
            t += Time.deltaTime;

            if (t >= CheckFrequency)
            {
                t = 0;

                if (DoorChecks.Length > 0)
                {
                    if (DoorChecks.All(x => x.HasMoved))
                    {
                        if (Targets.Any(x => x.On == false) && KillIfTargetsExcist)
                        {
                            if (Game.GetController().Exploded == false)
                            {
                                Game.GetController().Kill();
                                if(Message != "")
                                {
                                    Game.GetMiddleText().Force(Message, 5, false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
