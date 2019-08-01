using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* handle what happens when buttons are pressed, for normal menu interactions on the left hand.*/
public class OculusNativeInput : MonoBehaviour
{
    // the action boolean if a menu was opened
    //public SteamVR_Action_Boolean click;
    public GameObject pointer;                  // the pointer
    public GameObject canvas;                   // the canvas
    public bool UIToggleable = true;            // whether or not the canvas can be turned on and off
    private bool activated = false;             // whether the canvas is currently activated
   

    // Update is called once per frame
    private void Update()
    {
       if(OVRInput.GetDown(OVRInput.Button.Three))          // if UI pointer should be active
        {
            //Debug.Log("activated Pointer!");

            pointer.SetActive(true);
        } else
        {
            // trigger menu item
            // check to see if getActive is true
            pointer.SetActive(true);
            // trigger menu item
            //
        }

       if(OVRInput.GetDown(OVRInput.Button.Four))            // if the menu should currently be active
        {
            activated = !activated; // switching the state of the bool
        }

        if (UIToggleable) // if the canvas can be turned on and off
        {
            if (activated)
            {
                canvas.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
            }
        }

    }

    // whether the ui action was activated
    // whether the open menu action was activated
    

    





}
