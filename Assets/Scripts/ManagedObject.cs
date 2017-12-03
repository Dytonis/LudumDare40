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
        public List<UI.EventHandler> Subscriptions = new List<UI.EventHandler>();

        public virtual void Reset() { }
        public virtual void OnDeath() { }
        public virtual void OnDestroy() { }
        public virtual void OnShoot()
        {
        }

        public void SendAll(Action e)
        {
            ManagedObject[] managed = GameObject.FindObjectsOfType<ManagedObject>();
            managed.ToList().ForEach(x => x.Invoke(e.Method.Name, 0));
        }

        public void ThrowEvent(Event e)
        {
            Subscriptions.ForEach(x => x.EventThrow(this, e));
        }

        public virtual void Subscribe(LD40.UI.EventHandler eventHandler)
        {
            Subscriptions.Add(eventHandler);
        }
    }
}
