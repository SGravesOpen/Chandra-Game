using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A script to find the camera used to track rays from a pointer,
 * and set that camera/pointer/ray to be the event activator for the canvas.
 * Should be more appropriately named to FindEventCamera, but its fine.
 * This is used on the canvas in the pixel cas A scene, as it doesnt know
 * where/which event camera to use on the start of the scene. 
 */ 
public class FindInputModule : MonoBehaviour
{

    private Canvas canvas;              // the canvas to set the camera for
    private GameObject rightHand;       // the right hand that has the event camera.
    private bool alreadySet = false;    // whether this has already been done once.

    // Start is called before the first frame update
    void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();
        rightHand = GameObject.Find("Controller (Right)");

    } 

    // Update is called once per frame
    void Update()
    {
        // checking to make sure right hand exists
        if(rightHand == null)
        {
            rightHand = GameObject.Find("Controller (Right)");
            return;
        } else
        {
            setCamera(); 
        }
        
    }

    // a function that sets the event camera if it has not already been done.
    private void setCamera()
    {
        if(!alreadySet)
        {
            Camera eventCamera = rightHand.GetComponentInChildren<Camera>();
            canvas.worldCamera = eventCamera;
            alreadySet = true;
        }
    }
}
