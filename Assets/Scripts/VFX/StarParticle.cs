using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40.VFX
{
    public class StarParticle : MonoBehaviour
    {
        public float WrapRange;
        public Vector3 velocity;
        public Transform center;

        public void Update()
        {
            transform.position = transform.position + (velocity * Time.deltaTime * Time.timeScale);

            if (transform.position.x < -WrapRange + center.transform.position.x)
                transform.position = new Vector3(WrapRange + center.transform.position.x, -10, transform.position.z);
            if (transform.position.z < -WrapRange + center.transform.position.z)
                transform.position = new Vector3(transform.position.x, -10, WrapRange + center.transform.position.z);
            if (transform.position.x > WrapRange + 1 + center.transform.position.x)
                transform.position = new Vector3(-WrapRange+ 1 + center.transform.position.x, -10, transform.position.z);
            if (transform.position.z > WrapRange + 1 + center.transform.position.z)
                transform.position = new Vector3(transform.position.x, -10, -WrapRange+ 1 + center.transform.position.z);
        }
    }
}
