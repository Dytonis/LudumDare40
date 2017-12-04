using LD40.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleField : MonoBehaviour
{
    public StarParticle prefab;
    public Vector2 scaleRange;
    public float velocityRandomness;
    public int count;
    private Transform center;

    public void Start()
    {
        center = Camera.main.transform;

        for(int i = 0; i < count; i++)
        {
            StarParticle part = Instantiate(prefab) as StarParticle;
            part.transform.SetParent(transform);
            part.gameObject.hideFlags = HideFlags.HideInHierarchy;
            part.center = center;
            part.velocity = new Vector3(Random.Range(-velocityRandomness, velocityRandomness), 0, Random.Range(-velocityRandomness, velocityRandomness));
            part.transform.position = new Vector3(Random.Range(-300, 300), -10, Random.Range(-300, 300));
            part.transform.localScale = new Vector3(Random.Range(scaleRange.x, scaleRange.y), 1, Random.Range(scaleRange.x, scaleRange.y));
        }
    }
}
