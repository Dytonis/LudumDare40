using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LD40.VFX
{
    public class TextColorFlash : MonoBehaviour
    {
        public void FlashTextColor(Color c, float duration)
        {
            GetComponent<Text>().color = Color.white;
            GetComponent<Text>().canvasRenderer.SetColor(c);
            GetComponent<Text>().CrossFadeColor(Color.white, duration, true, true);
        }
    }
}
