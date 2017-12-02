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
    public class TutorialPopMessageBox : ResetableObject
    {
        public string Message;
        public float Time;
        public bool DisableTriggerOnHit;

        public Text Fill; //dont break something that works
        public Text BG;

        private BoxCollider Trigger;

        public void Start()
        {
            Trigger = GetComponent<BoxCollider>();
        }

        public void OnTriggerEnter(Collider col)
        {
            StopAllCoroutines();
            StartCoroutine(PopMessage(Message, Time));
            Trigger.enabled = !DisableTriggerOnHit;
        }

        public IEnumerator PopMessage(string message, float time)
        {
            Fill.text = message;
            BG.text = message;

            Fill.color = Color.white;
            BG.color = Color.black;

            Fill.canvasRenderer.SetColor(Color.clear);
            BG.canvasRenderer.SetColor(Color.clear);

            Fill.CrossFadeColor(Color.white, 1, true, true);
            BG.CrossFadeColor(Color.black, 1, true, true);

            yield return new WaitForSecondsRealtime(time);

            if (Fill.text != message)
                yield break;

            Fill.CrossFadeColor(Color.clear, 1, true, true);
            BG.CrossFadeColor(Color.clear, 1, true, true);
        }

        public override void Reset()
        {
            Trigger.enabled = true;
        }
    }
}
