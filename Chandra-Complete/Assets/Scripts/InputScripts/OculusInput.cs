using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/* handle what happens when buttons are pressed, for normal menu interactions on the left hand.*/
public class OculusInput : MonoBehaviour
{
    public SteamVR_Input_Sources handType;      // input hand type
    public SteamVR_Action_Boolean uiAction;     // the action boolean for if a menu action was triggered
    public SteamVR_Action_Boolean openMenu;     // the action boolean if a menu was opened
    //public SteamVR_Action_Boolean click;
    public GameObject pointer;                  // the pointer
    public GameObject canvas;                   // the canvas
    public bool UIToggleable = true;            // whether or not the canvas can be turned on and off
    private bool activated = false;             // whether the canvas is currently activated
   

    // Update is called once per frame
    void Update()
    {
       if(GetUIDown())          // if UI pointer should be active
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

       if(GetMenu())            // if the menu should currently be active
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
    public bool GetUIDown() 
    {
        //return OVRInput.GetDown(OVRInput.RawButton.Y);
        return uiAction.GetState(handType);   
    }

    // whether the open menu action was activated
    public bool GetMenu()
    {
        //return OVRInput.GetDown(OVRInput.RawButton.X);
        return openMenu.GetStateDown(handType); 
    }

    





}
