using LD40;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : ResetableObject
{
    public ParticleSystem system;
    public Vector3 offset;

    private Vector3 target;

    private bool lowering;
    private Vector3 start;

    public void Start()
    {
        target = transform.position + offset;
        start = transform.position;
    }

    public void Update()
    {
        if(lowering)
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * Time.timeScale * 4);
        if (Vector3.Distance(transform.position, target) < 1)
            system.gameObject.SetActive(false);
    }

    public void StartSystem()
    {
        system.gameObject.SetActive(true);
    }

    public void LowerGate()
    {
        lowering = true;   
        StartSystem();
    }

    public override void Reset()
    {
        transform.position = start;
        system.gameObject.SetActive(false);
        lowering = false;
    }
}
