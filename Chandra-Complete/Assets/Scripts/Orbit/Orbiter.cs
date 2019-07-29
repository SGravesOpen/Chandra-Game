using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public abstract class Orbiter : MonoBehaviour, IOrbit
{
    private Orbiter[] orbiters;

    public Rigidbody rb;

    public float G = .00000000006674f;

    public float weights = 100;

    

    // Start is called before the first frame update
    void Start()
    {
        orbiters = FindObjectsOfType<Orbiter>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Orbiter o in orbiters) {
            Debug.Log(orbiters.Length);
            if(o != this) {
                gravitate(o);
            }
        }
    }

    public abstract void gravitate(Orbiter o);
}
