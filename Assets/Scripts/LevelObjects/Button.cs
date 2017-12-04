using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD40.Events;
using UnityEngine;

namespace LD40.LevelObjects
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Events.EventHandler))]
    public class Button : ManagedObject
    {
        public GameObject OnButton;
        public GameObject OffButton;

        public bool DeactivatedByTime;
        public float DeactivationTime;
        public bool CanBeActivatedByDrone;

        public bool On;

        public override void OnReset()
        {
            Deactivate();
        }

        public override void OnManagedEvent(ManagedEventType e)
        {
            if(e == ManagedEventType.OnProjectileHit || (e == ManagedEventType.OnDroneProjectileHit && CanBeActivatedByDrone))
            {
                Activate();
            }
        }

        public void Activate()
        {
            if (On == false)
            {
                OnButton.SetActive(true);
                OffButton.SetActive(false);
                On = true;
                ThrowEvent(Events.ManagedEventType.OnButtonActivated);
                Game.GetAudio().ButtonBoop.Play();
            }
        }

        public void Deactivate()
        {
            if (On == true)
            {
                OnButton.SetActive(false);
                OffButton.SetActive(true);
                On = false;
                ThrowEvent(Events.ManagedEventType.OnButtonDeactivated);
            }
        }
    }
}
