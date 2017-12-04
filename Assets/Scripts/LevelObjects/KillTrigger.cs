using LD40;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using LD40.Events;

namespace LD40.LevelObjects
{
    [RequireComponent(typeof(Events.EventHandler))]
    [RequireComponent(typeof(BoxCollider))]
    public class KillTrigger : ManagedObject
    {
        [HideInInspector]
        public bool Inside;

        public string Message;

        public void OnTriggerEnter(Collider col)
        {
            if(col.tag == "Tank")
            {
                Inside = true;
            }
        }
        public void OnTriggerExit(Collider col)
        {
            if (col.tag == "Tank")
            {
                Inside = false;
            }
        }

        public override void OnManagedEvent(ManagedEventType e)
        {
            if (Inside && Game.GetController().Exploded == false)
            {
                Game.GetController().Kill();
                if (Message != "")
                    Game.GetMiddleText().Force(Message, 5, false);
            }
        }
    }
}
