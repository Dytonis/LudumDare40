using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LD40.UI
{
    [RequireComponent(typeof(BoxCollider))]
    public class TutorialPopMessageBox : ManagedObject
    {
        public string Message;
        public float Time;
        public bool DisableTriggerOnExit;
        public bool StayWhileIn;
        public bool StayDisabled;

        private BoxCollider Trigger;
        private bool timeout = false;

        public void Start()
        {
            Trigger = GetComponent<BoxCollider>();
        }

        public void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Tank")
            {
                Game.GetMiddleText().AddToQueue(Message, Time, StayWhileIn);
            }
        }

        public void OnTriggerExit(Collider col)
        {
            if (timeout == false && col.tag == "Tank" && Game.GetController().Exploded == false)
            {
                Trigger.enabled = !DisableTriggerOnExit;
                if (StayWhileIn)
                {
                    Game.GetMiddleText().ForceNextMessage();
                }
            }
        }

        public override void OnReset()
        {
            if (!StayDisabled)
            {
                Trigger.enabled = true;
                //for any colliders that get disabled when we get moved out of them due to respawn
                StartCoroutine(Timeout());
            }
        }

        public IEnumerator Timeout()
        {
            timeout = true;
            yield return new WaitForSecondsRealtime(0.15f);
            timeout = false;
        }
    }
}
