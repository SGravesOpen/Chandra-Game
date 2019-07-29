using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/* a script to enable flying. uses SteamVR, and the 
 * inner triggers to fly
 */
public class Flying : MonoBehaviour
{

    private Transform head;                     // transform of the head/main camera
    private Transform cameraRig;                // the transform of the cameraRig

    public SteamVR_Input_Sources leftHand;      // the left hand for the SteamVR input
    public SteamVR_Input_Sources rightHand;     // the right hand for the SteamVR input
    public SteamVR_Action_Boolean gripClick;    // the action boolean set for it a grip is pressed


    // Start is called before the first frame update
    void Start()
    {
        // replace head with whatever game object's name you want to aim with.
        head = GameObject.Find("Camera").transform;
        cameraRig = GameObject.Find("MainRig").transform;
    }

    // Update is called once per frame
    void Update()
    {

        // if stuff is null, dont try anything and try to find them again
        if(head == null || cameraRig == null)
        {
            Debug.Log("Somethings null playa!");
            head = GameObject.Find("Camera").transform;
            cameraRig = GameObject.Find("MainRig").transform;
            return;
        }


        // making sure both triggers are clicked
        if(GetGripClick(leftHand) && GetGripClick(rightHand))
        {
            // this is directing flight based on head direction.
            Vector3 forward = head.transform.forward * 1;
            cameraRig.transform.position += forward;

        }
        
    }

    // returns whether or not a hand grip is currently pressed.
    public bool GetGripClick(SteamVR_Input_Sources h)
    {
        return gripClick.GetState(h);
    }


}
