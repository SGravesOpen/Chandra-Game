using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOrbiter : Orbiter
{
    public override void gravitate(Orbiter o)
    {
        Vector3 direction = rb.position - o.rb.position;
        Vector3 norm_dir = direction.normalized;
        float distance = direction.magnitude;

        float gravity = G * (rb.mass * o.rb.mass * weights)
            / Mathf.Pow(distance, 2f);
        Debug.Log(gravity);

        Vector3 gravityVector = (gravity * norm_dir * distance/1000f);
        o.rb.AddForce(gravityVector);
        Debug.Log(gravityVector);
    }

}
