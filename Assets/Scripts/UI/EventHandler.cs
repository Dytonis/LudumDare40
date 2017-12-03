using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace LD40.UI
{
    public class EventHandler : MonoBehaviour
    {
        public Event TargetEvent;
        [HideInInspector]
        public ManagedObject ManagedTarget;
        [HideInInspector]
        public bool RequiredTarget = false;

        public void Start()
        {
            if (TargetEvent != Event.None)
            {
                if (ManagedTarget)
                {
                    ManagedTarget.Subscribe(this);
                    Debug.Log(name + " has subscribe to managed event handler " + ManagedTarget.name + " listening for: " + TargetEvent.ToString());
                }
                else if (RequiredTarget)
                {
                    Debug.Log(name + " could not subscribe to managed event. There was no target defined.");
                }
                else
                {
                    ManagedObject[] managed = GameObject.FindObjectsOfType<ManagedObject>();

                    foreach (ManagedObject obj in managed)
                    {
                        obj.Subscribe(this);
                    }
                    Debug.Log(name + " has subscribe to global event handler " + TargetEvent.ToString());
                }
            }
        }

        public void EventThrow(ManagedObject sender, Event e)
        {
            if(ManagedTarget)
            {
                if(ManagedTarget == sender && e == TargetEvent)
                {
                    Debug.Log("Recieved managed event command " + e.ToString() + " from sender " + sender.name + ".");
                    Trigger();
                }
            }
            else if (e == TargetEvent)
            {
                Debug.Log("Recieved event command " + e.ToString() + " from sender " + sender.name + ".");
                Trigger();
            }
        }

        public virtual void Trigger()
        {

        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(EventHandler))]
    public class EventHandlerEditor : Editor
    {
        public Event[] ManagedEvents = new Event[]
        {
            Event.OnShoot,
            Event.OnDeath,
            Event.OnReset,
            Event.OnFirstShot,
            Event.OnTargetDestroy,
        };

        public void OnGui()
        {
            EventHandler myScript = target as EventHandler;

            Event selection = (Event)EditorGUILayout.EnumPopup("Target Event ", myScript.TargetEvent);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EventActivatedText myScript = target as EventActivatedText;

            if (ManagedEvents.Any(x => x == myScript.TargetEvent))
            {
                myScript.RequiredTarget = true;
                myScript.ManagedTarget = EditorGUILayout.ObjectField("Target Object ", myScript.ManagedTarget, typeof(ManagedObject), true) as ManagedObject;
            }
            else
            {
                myScript.RequiredTarget = false;
            }
        }
    }
#endif
}
