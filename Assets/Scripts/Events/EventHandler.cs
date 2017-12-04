using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace LD40.Events
{
    public class EventHandler : MonoBehaviour
    {
        public ManagedEventType TargetEvent;
        [HideInInspector]
        public ManagedObject ManagedTarget;
        [HideInInspector]
        public bool RequiredTarget = false;
        [HideInInspector]
        public bool SelfManaged;

        public void Start()
        {
            if (TargetEvent != ManagedEventType.None)
            {
                if (ManagedTarget)
                {
                    ManagedTarget.Subscribe(this);
                    Debug.Log(name + " has subscribed to managed event handler " + ManagedTarget.name + " listening for: " + TargetEvent.ToString());
                }
                else if (RequiredTarget)
                {
                    Debug.Log(name + " could not subscribe to managed event. There was no target defined.");
                }
                else if(GetComponent<ManagedObject>())
                {
                    GetComponent<ManagedObject>().Subscribe(this);

                    Debug.Log(name + " has subscribed to global event handler " + TargetEvent.ToString());
                }
                else
                {
                    Debug.Log(name + " could not subscribe to managed event. There was no ManagedObject attached to this GameObject.");
                }
            }
        }

        public void EventThrow(ManagedObject sender, ManagedEventType e)
        {
            if(ManagedTarget)
            {
                if(ManagedTarget == sender && (e == TargetEvent || TargetEvent == ManagedEventType.OnAny))
                {
                    Debug.Log(name + " recieved managed event command " + e.ToString() + " from sender " + sender.name + ".");
                    Trigger();
                }
            }
            else if (e == TargetEvent || TargetEvent == ManagedEventType.OnAny)
            {
                Debug.Log(name + " recieved event command " + e.ToString() + " from sender " + sender.name + ".");
                Trigger();
            }
        }

        public virtual void Trigger()
        {
            if (GetComponent<ManagedObject>())
                GetComponent<ManagedObject>().OnManagedEvent(TargetEvent);
        }
    }

    public enum ManagedEventType
    {
        None,
        OnDeath,
        OnReset,
        OnShoot,
        OnFirstShot,
        OnTargetDestroy,
        OnButtonActivated,
        OnButtonDeactivated,
        OnProjectileHit,
        OnDoorMoved,
        OnDroneProjectileHit,
        OnAny
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(EventHandler))]
    public class EventHandlerEditor : Editor
    {
        public ManagedEventType[] ManagedEvents = new ManagedEventType[]
        {
            ManagedEventType.OnShoot,
            ManagedEventType.OnDeath,
            ManagedEventType.OnReset,
            ManagedEventType.OnFirstShot,
            ManagedEventType.OnTargetDestroy,
            ManagedEventType.OnButtonActivated,
            ManagedEventType.OnButtonDeactivated,
            ManagedEventType.OnDoorMoved
        };

        public void OnGui()
        {
            EventHandler myScript = target as EventHandler;

            ManagedEventType selection = (ManagedEventType)EditorGUILayout.EnumPopup("Target Event ", myScript.TargetEvent);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EventHandler myScript = target as EventHandler;

            if (ManagedEvents.Any(x => x == myScript.TargetEvent))
            {
                myScript.RequiredTarget = true;
                myScript.SelfManaged = EditorGUILayout.Toggle("Self Managed? ", myScript.SelfManaged);
                if (myScript.SelfManaged)
                {
                    EditorGUILayout.LabelField("Managed Object is Self Managed");
                }
                else
                {
                    myScript.ManagedTarget = EditorGUILayout.ObjectField("Target Object ", myScript.ManagedTarget, typeof(ManagedObject), true) as ManagedObject;
                }
            }
            else
            {
                myScript.RequiredTarget = false;
            }
        }
    }
#endif
}
