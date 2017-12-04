using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD40.Events;
using UnityEngine;
using System.Collections;

namespace LD40.LevelObjects
{
    [RequireComponent(typeof(Events.EventHandler))]
    public class Door : ManagedObject
    {
        private Events.EventHandler EventHandle;

        public bool OpenOnEventTrigger;

        public GameObject RightDoor;
        public GameObject LeftDoor;

        [HideInInspector]
        public bool HasMoved;

        public Vector3 RightRotationOpen;
        public Vector3 LeftRotationOpen;
        public Vector3 RightRotationClosed;
        public Vector3 LeftRotationClosed;

        public void Start()
        {
            EventHandle = GetComponent<Events.EventHandler>();
        }

        public override void OnManagedEvent(ManagedEventType e)
        {
            if (OpenOnEventTrigger)
                OpenDoor();
        }

        public override void OnReset()
        {
            LeftDoor.transform.localRotation = Quaternion.Euler(LeftRotationClosed);
            RightDoor.transform.localRotation = Quaternion.Euler(RightRotationClosed);
            StopAllCoroutines();
            HasMoved = false;
        }

        public void OpenDoor()
        {
            StartCoroutine(OpenLeftDoor());
            StartCoroutine(OpenRightDoor());
        }

        public void CloseDoor()
        {

        }

        private IEnumerator OpenLeftDoor()
        {
            while (true)
            {
                LeftDoor.transform.localRotation = Quaternion.RotateTowards(LeftDoor.transform.localRotation, Quaternion.Euler(LeftRotationOpen), Time.deltaTime * 30 * Time.timeScale);
                yield return new WaitForEndOfFrame();

                if (LeftDoor.transform.localRotation == Quaternion.Euler(LeftRotationOpen))
                {
                    ThrowEvent(ManagedEventType.OnDoorMoved);
                    HasMoved = true;
                    yield break;                  
                }
            }
        }

        private IEnumerator OpenRightDoor()
        {
            while (true)
            {
                RightDoor.transform.localRotation = Quaternion.RotateTowards(RightDoor.transform.localRotation, Quaternion.Euler(RightRotationOpen), Time.deltaTime * 30 * Time.timeScale);
                yield return new WaitForEndOfFrame();

                if (RightDoor.transform.localRotation == Quaternion.Euler(RightRotationOpen))
                {
                    ThrowEvent(ManagedEventType.OnDoorMoved);
                    HasMoved = true;
                    yield break;
                }
            }         
        }

        private IEnumerator CloseLeftDoor()
        {
            while (true)
            {
                LeftDoor.transform.rotation = Quaternion.RotateTowards(LeftDoor.transform.rotation, Quaternion.Euler(LeftRotationClosed), Time.deltaTime * 30 * Time.timeScale);
                yield return new WaitForEndOfFrame();

                if (LeftDoor.transform.rotation == Quaternion.Euler(LeftRotationClosed))
                {
                    ThrowEvent(ManagedEventType.OnDoorMoved);
                    HasMoved = true;
                    yield break;                   
                }
            }
        }

        private IEnumerator CloseRightDoor()
        {
            while (true)
            {
                RightDoor.transform.rotation = Quaternion.RotateTowards(RightDoor.transform.rotation, Quaternion.Euler(RightRotationClosed), Time.deltaTime * 30 * Time.timeScale);
                yield return new WaitForEndOfFrame();

                if (RightDoor.transform.rotation == Quaternion.Euler(RightRotationClosed))
                {
                    ThrowEvent(ManagedEventType.OnDoorMoved);
                    HasMoved = true;
                    yield break;
                }
            }
        }
    }
}
