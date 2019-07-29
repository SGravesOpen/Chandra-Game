using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOSight : MonoBehaviour
{

    private RaycastHit vision;
    private float rayLength;
    

    // Start is called before the first frame update
    void Start()
    {
        rayLength = Mathf.Infinity;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        // Debug.DrawRay(this.transform.position, this.transform.forward * rayLength, Color.red, 2f);
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * Mathf.Infinity, Color.yellow, Time.deltaTime, false);
            Debug.Log(hit.collider.transform.name);
        }
    }
}
