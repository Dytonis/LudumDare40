using LD40.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40
{
    public abstract class ManagedObject : MonoBehaviour
    {
        [HideInInspector]
        public List<LD40.Events.EventHandler> Subscriptions = new List<LD40.Events.EventHandler>();

        public virtual void OnReset() { }
        public virtual void OnDeath() { }
        public virtual void OnDestroy() { }
        public virtual void OnShoot() { }
        public virtual void OnManagedEvent(Events.ManagedEventType e) { }

        public void SendAll(Action e)
        {
            ManagedObject[] managed = GameObject.FindObjectsOfType<ManagedObject>();
            managed.ToList().ForEach(x => x.Invoke(e.Method.Name, 0));
        }

        public void ThrowEvent(ManagedEventType e)
        {
            Subscriptions.ForEach(x => x.EventThrow(this, e));
        }

        public virtual void Subscribe(LD40.Events.EventHandler eventHandler)
        {
            Subscriptions.Add(eventHandler);
        }
    }
}
