using LD40;
using LD40.Events;
using LD40.UI;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class EventActivatedText : EventHandler
{
    public Text Fill;
    public Text BG;

    [HideInInspector]
    public string text;
    [HideInInspector]
    public float time;
    public bool ForceTextQueueClear;

    public override void Trigger()
    {
        if (ForceTextQueueClear)
        {
            Game.GetMiddleText().Force(text, time, false);
        }
        else
        {
            Game.GetMiddleText().AddToQueue(text, time, false);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EventActivatedText))]
public class EventActivatedTextEditor : EventHandlerEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EventActivatedText myScript = target as EventActivatedText;

        if(myScript.TargetEvent != ManagedEventType.None)
        {
            myScript.text = EditorGUILayout.TextArea(myScript.text, GUILayout.Height(60));
            myScript.time = EditorGUILayout.FloatField("Stay Time ", myScript.time);
        }
    }
}
#endif
