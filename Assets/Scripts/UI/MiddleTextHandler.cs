using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LD40.UI
{
    public class MiddleTextHandler : MonoBehaviour
    {
        public List<MiddleTextQueue> Queue = new List<MiddleTextQueue>();

        public float FadeTime = 0.25f;

        public Text BG;
        public Text Fill;

        private bool fading;
        private bool skipRemove;

        private void NextMessage()
        {
            if(Queue.Count > 0)
            {
                if (!skipRemove)
                    Queue.RemoveAt(0);
                else
                    skipRemove = false;
            }

            if (Queue.Count > 0)
            {
                StartCoroutine(IE_PopMessage(Queue[0].text, Queue[0].time, Queue[0].stay));
            }
            else
            {
                Fill.CrossFadeColor(Color.clear, FadeTime, true, true);
                BG.CrossFadeColor(Color.clear, FadeTime, true, true);
            }
        }

        private void StartFirst()
        {
            if (Queue.Count > 0)
            {
                StartCoroutine(IE_PopMessage(Queue[0].text, Queue[0].time, Queue[0].stay));
            }
        }

        public void ClearQueue()
        {
            Queue.Clear();
        }

        public void Force(string message, float time, bool always)
        {
            if(fading)
            {
                skipRemove = true;
            }
            Queue.Clear();
            Queue.Add(new MiddleTextQueue() { text = message, time = time, stay = always });
            if (Fill.canvasRenderer.GetAlpha() > 0.1f)
            {
                StartCoroutine(IE_HideThenStart());
            }
            else
            {
                StartFirst();
            }
        }

        public void ForceNextMessage()
        {
            StartCoroutine(IE_HideThenNext());
        }

        public void Hide()
        {
            Fill.CrossFadeColor(Color.clear, FadeTime, true, true);
            BG.CrossFadeColor(Color.clear, FadeTime, true, true);
        }

        private IEnumerator IE_HideThenNext()
        {
            fading = true;
            Fill.CrossFadeColor(Color.clear, FadeTime, true, true);
            BG.CrossFadeColor(Color.clear, FadeTime, true, true);
            yield return new WaitForSecondsRealtime(FadeTime);
            fading = false;
            NextMessage();
        }

        private IEnumerator IE_HideThenStart()
        {
            fading = true;
            Fill.CrossFadeColor(Color.clear, FadeTime, true, true);
            BG.CrossFadeColor(Color.clear, FadeTime, true, true);
            yield return new WaitForSecondsRealtime(FadeTime);
            fading = false;
            StartFirst();
        }

        public void AddToQueue(string message, float time, bool always)
        {
            if (Queue.Count <= 0)
            {
                Queue.Add(new MiddleTextQueue() { text = message, time = time, stay = always });
                StartFirst();
            }
            else
            {
                Queue.Add(new MiddleTextQueue() { text = message, time = time, stay = always });
            }
        }

        private IEnumerator IE_PopMessage(string message, float time, bool always)
        {
            Fill.text = message;
            BG.text = message;

            Fill.color = Color.white;
            BG.color = Color.black;

            Fill.canvasRenderer.SetColor(Color.clear);
            BG.canvasRenderer.SetColor(Color.clear);

            Fill.CrossFadeColor(Color.white, FadeTime, true, true);
            BG.CrossFadeColor(Color.black, FadeTime, true, true);

            if (!always)
            {
                yield return new WaitForSecondsRealtime(time);

                if (Fill.text != message)
                    yield break;

                StartCoroutine(IE_HideThenNext());
            }
        }
    }

    [System.Serializable]
    public struct MiddleTextQueue
    {
        public string text;
        public float time;
        public bool stay;
    }
}
