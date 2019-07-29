using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOrbiter : Orbiter
{
    public override void gravitate(Orbiter o)
    {
       

        Vector3 direction = rb.position - o.rb.position;
        Vector3 norm_dir = direction.normalized;
        float distance = direction.magnitude;

        Vector3 perp = Vector3.Cross(direction, transform.up).normalized;


        float gravity = G * (rb.mass * o.rb.mass * weights)
                             / Mathf.Pow(distance, 2f);
        //Debug.Log(gravity);

        rb.AddForce(perp*15f);

        Vector3 gravityVector = (gravity * norm_dir);
        o.rb.AddForce(gravityVector);
        //Debug.Log(gravityVector);


    }
}
