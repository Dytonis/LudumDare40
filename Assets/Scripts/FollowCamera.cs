using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Anchor;
    public Vector3 Offset;
	
	// Update is called once per frame
	void Update ()
    {
        if(Anchor)
            transform.position = Anchor.position + Offset;
	}
}
