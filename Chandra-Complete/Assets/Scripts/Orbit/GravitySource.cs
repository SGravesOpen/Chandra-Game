using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))] 
public class GravitySource : MonoBehaviour
{
    // for VR
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean actionOne;

    public float gravity = .006674f; // the gravity const value
    private float G = .00000000006674f; // other that is stronger
    private Rigidbody rb; // the rb of the earth/ planet with attractive f.
    private float thrusters = 100f; // boosters to add to perpindicular force
    private float pull = 1f; // slowly increase to make orbiters crash
    private Rigidbody exiter; // rb of the object that leaves the gravity effects
    private bool orbiting = true; // if the orbiting ship is still orbiting
    private float orbital_force; // force that is going around
    private Vector3 perp; // perpindicular direction to gravity
    public GameObject firePoint;
    public GameObject effectToSpawn;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Start");
    }

    private void FixedUpdate()
    {
        if(orbiting) {
            pull += .25f;
            thrusters -= .5f;

            if (GetActionOne()){
                pull -= .5f;
                Debug.Log("Thrust!");
                thrusters += 15f;
                SpawnVFX();

                // 

            }

            if(thrusters < 100f) {
                thrusters = 100f;
            }

            if (pull < 1f) {
                pull = 1f;
            }
            Debug.Log("Thruster: " + thrusters);
            //Debug.Log("pull: " + pull);
        } else {
            exiter.AddForce(orbital_force * perp, ForceMode.Acceleration);
        }
    }


    void OnTriggerStay(Collider other)
    {
        Rigidbody orb = other.GetComponent<Rigidbody>();
        if(other.tag == "orbiter" && orb != null) {

            Vector3 difference = gameObject.transform.position - other.gameObject.transform.position;
  
            float dist = difference.magnitude;
            //Debug.Log("Distance: " + dist);
            Vector3 gravityDirection = difference.normalized;
            //Debug.Log("Gravity Direction: " + gravityDirection);

            perp = Vector3.Cross(gravityDirection, transform.up).normalized;
            //Debug.Log("perp: " + perp);
            Vector3 gravityVector = (gravityDirection * gravity * rb.mass * 1000) / (dist * dist);
            //Debug.Log("Gravity Vector: " + gravityVector);

            float orbital_velocity = Mathf.Sqrt(gravity * rb.mass / dist);
            orbital_force = thrusters * orb.mass * (orbital_velocity / Time.fixedDeltaTime);
            //Debug.Log("Orbital Force: " + orbital_force);

            orb.AddForce(gravityVector * pull, ForceMode.Acceleration);
            orb.AddForce(orbital_force * perp, ForceMode.Acceleration);

        } else if(other.tag == "planet" && orb != null) {
            // do stuff for other planets
        } else {
            Debug.Log("please attach a rigidbody to orbiting object.");
        }
       

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "orbiter")
        {
            exiter = other.gameObject.GetComponent<Rigidbody>();
            orbiting = false;
            Debug.Log("Exited");
        }
    }


    // method to check if thruster is activated!
    public bool GetActionOne()
    {
        return actionOne.GetStateDown(handType);
    }


    public void SpawnVFX()
    {
        GameObject vfx;

        if (firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
       
        var ps = vfx.GetComponent<ParticleSystem>();

        if (vfx.transform.childCount > 0)
        {
            ps = vfx.transform.GetChild(0).GetComponent<ParticleSystem>();
        }
    }

}
}

