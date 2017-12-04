using LD40;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD40.Events;

public class MoveObjectOnEvent : ManagedObject
{
    public float Speed;
    public Transform Start;
    public Transform End;

    public Transform Move;

    private bool A1 = false;

    public override void OnManagedEvent(ManagedEventType e)
    {
        A1 = true;
    }

    public override void OnReset()
    {
        Move.localPosition = Start.localPosition;
    }

    public void Update()
    {
        if (A1)
        {
            Move.localPosition = Vector3.MoveTowards(Move.localPosition, End.localPosition, Time.deltaTime * Time.timeScale * Speed);
            if (Vector3.Distance(Move.position, End.position) <= 0.1f)
            {
                A1 = false;
            }
        }
    }
}
