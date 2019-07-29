using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrbitMotion))]
[RequireComponent(typeof(Rigidbody))]
public class Plummet : MonoBehaviour
{
    private OrbitMotion orbitMotion;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        orbitMotion = GetComponent<OrbitMotion>();
        StartCoroutine(PlummetToEarth());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("space pressed");
            orbitMotion.orbitPath.xAxis += .5f;
            orbitMotion.orbitPath.yAxis += .5f;

            if(orbitMotion.orbitPath.xAxis > 40 || orbitMotion.orbitPath.yAxis > 40) {
                orbitMotion.isOrbiting = false;
                StopCoroutine(PlummetToEarth());

                Vector3 perp = Vector3.Cross(rb.angularVelocity, transform.up).normalized;

                rb.AddForce(perp);
            }
        }
        
    }

    IEnumerator PlummetToEarth() {
        while (true)
        {
            Debug.Log(orbitMotion.orbitPath.xAxis);
            Debug.Log(orbitMotion.orbitPath.yAxis);
            orbitMotion.orbitPath.xAxis -= .05f;
            orbitMotion.orbitPath.yAxis -= .05f;
            yield return new WaitForSeconds(.1f);
        }
    }
}
